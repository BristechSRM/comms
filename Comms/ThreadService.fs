module Comms.ThreadService

open Amazon.DynamoDBv2
open Amazon
open Amazon.DynamoDBv2.DataModel
open Amazon.DynamoDBv2.DocumentModel
open System
open Serilog
open Comms.Models
open Comms.Entities

let client = new AmazonDynamoDBClient(RegionEndpoint.EUWest1)
let context = new DynamoDBContext(client)

let entityToCorrespondence (entity : CorrespondenceEntity) : Correspondence = 
    { SenderId = entity.SenderId
      ReceiverId = entity.ReceiverId
      Type = entity.Type
      SenderHandle = entity.SenderHandle
      ReceiverHandle = entity.ReceiverHandle
      Date = entity.Date
      Message = entity.Message }

let getEntityItems (id : string) : seq<Correspondence> = 
    Log.Information("Contacting DynamoDB for correspondence items with ThreadId: {id}", id)
    context.Scan<CorrespondenceEntity>(new ScanCondition("ThreadId", ScanOperator.Equal, id))
    |> Seq.cast
    |> Seq.map entityToCorrespondence

let entityToThread (entity : ThreadEntity) : ThreadDetail = 
    Log.Information("{S}", entity)
    { Id = entity.Id
      Items = entity.Id |> getEntityItems }


let getThread (id : string) = 
    Log.Information("Contacting DynamoDB for thread with Id: {id}", id)
    let thread = context.Load<ThreadEntity>(id)
    if box thread = null then None
        else Some (entityToThread thread)
        

let getThreads() = 
    Log.Information("Contacting DynamoDB for threads")
    context.Scan<ThreadEntity>() |> Seq.map entityToThread