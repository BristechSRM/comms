module Comms.ThreadService

open Amazon.DynamoDBv2
open Amazon
open Amazon.DynamoDBv2.DataModel
open Amazon.DynamoDBv2.DocumentModel
open Comms.Models
open System
open Serilog
open Comms.Models

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
    Log.Information("Contacting DynamoDB for correspondence with {id}", id)
    let x = context.Scan<CorrespondenceEntity>(new ScanCondition("ThreadId", ScanOperator.Equal, id))
    x
    |> Seq.cast
    |> Seq.map entityToCorrespondence

let entityToThread (entity : ThreadEntity) : ThreadDetail = 
    { Id = entity.Id
      Items = entity.Id |> getEntityItems }

let getThread (id : string) = 
    Log.Information("Contacting DynamoDB for thread with $id", id)
    context.Scan<ThreadDetail>(new ScanCondition("Id", ScanOperator.Equal, id))

let getThreads() = 
    Log.Information("Contacting DynamoDB for threads")
    context.Scan<ThreadEntity>() |> Seq.map entityToThread
