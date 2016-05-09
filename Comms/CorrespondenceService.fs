module Comms.CorrespondenceService

open Amazon
open Amazon.DynamoDBv2
open Amazon.DynamoDBv2.DataModel
open Amazon.DynamoDBv2.DocumentModel
open Comms.Entities
open Comms.LastContactService
open Serilog
open System

let client = new AmazonDynamoDBClient(RegionEndpoint.EUWest1)
let context = new DynamoDBContext(client)

let itemWithNewId (item : CorrespondenceItemEntity) = { item with Id = Guid.NewGuid().ToString() }

let getExternalIdsByHandle (handle : string) = 
    let senderResults = context.Scan<CorrespondenceItemEntity>(ScanCondition("SenderHandle",ScanOperator.Equal,handle))
    let receiverResults = context.Scan<CorrespondenceItemEntity>(ScanCondition("ReceiverHandle",ScanOperator.Equal,handle))

    [senderResults;receiverResults]
    |> Seq.concat
    |> Seq.map(fun x -> x.ExternalId)

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
    let entity = itemWithNewId item
    context.Save(entity)
    let item = context.Load<CorrespondenceItemEntity>(entity.Id)
    match box item |> isNull with
    | true -> None
    | false -> 
        updateLastContact item
        Some item.Id
