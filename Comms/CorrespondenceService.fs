module Comms.CorrespondenceService

open Amazon
open Amazon.DynamoDBv2
open Amazon.DynamoDBv2.DataModel
open Amazon.DynamoDBv2.DocumentModel
open Comms.Entities
open Comms.LastContactService
open Comms.Models
open Serilog
open System
open System.Net

let client = new AmazonDynamoDBClient(RegionEndpoint.EUWest1)
let context = new DynamoDBContext(client)
let itemWithNewId (item : CorrespondenceItemEntity) = { item with Id = Guid.NewGuid().ToString() }

let getExternalIdsByHandle (handle : string) = 
    let senderResults = context.Scan<CorrespondenceItemEntity>(ScanCondition("SenderHandle", ScanOperator.Equal, handle))
    let receiverResults = context.Scan<CorrespondenceItemEntity>(ScanCondition("ReceiverHandle", ScanOperator.Equal, handle))
    [ senderResults; receiverResults ]
    |> Seq.concat
    |> Seq.map (fun x -> x.ExternalId)

let getCorrespondence (profileIdOne : string) (profileIdTwo : string) = 
    Log.Information("Getting all correspondence between {id1} and {id2}", profileIdOne, profileIdTwo)
    let firstResult = 
        context.Scan<CorrespondenceItemEntity>(ScanCondition("SenderId", ScanOperator.Equal, profileIdOne), ScanCondition("ReceiverId", ScanOperator.Equal, profileIdTwo))
    let secondResult = 
        context.Scan<CorrespondenceItemEntity>(ScanCondition("SenderId", ScanOperator.Equal, profileIdTwo), ScanCondition("ReceiverId", ScanOperator.Equal, profileIdOne))
    [ firstResult; secondResult ]
    |> Seq.concat
    |> Seq.sortByDescending (fun x -> x.Date)

let createCorrespondenceItem (item : CorrespondenceItemEntity) = 
    Log.Information("Saving correspondence between {id1} and {id2}", item.ReceiverId, item.SenderId)
    if String.IsNullOrWhiteSpace item.ExternalId then 
        Failure { HttpStatus = HttpStatusCode.BadRequest
                  Message = "External Id for item was null. Item must contain a unique external id." }
    else 
        let itemExists = 
            context.Scan<CorrespondenceItemEntity>(ScanCondition("ExternalId", ScanOperator.Equal, item.ExternalId))
            |> Seq.isEmpty
            |> not
        if itemExists then 
            Failure { HttpStatus = enum 422
                      Message = "Item already exists. A new correspondence item was not created." }
        else 
            let entity = itemWithNewId item
            context.Save(entity)
            let item = context.Load<CorrespondenceItemEntity>(entity.Id)
            match box item |> isNull with
            | true -> 
                Failure { HttpStatus = HttpStatusCode.InternalServerError
                          Message = "Internal Server Error. A new correspondence item was not created." }
            | false -> 
                updateLastContact item
                Success item.Id
