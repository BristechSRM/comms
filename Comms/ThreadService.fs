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

let entityToCorrespondenceItem (entity : CorrespondenceItemEntity) : CorrespondenceItem = 
    { SenderId = entity.SenderId
      ReceiverId = entity.ReceiverId
      Type = entity.Type
      SenderHandle = entity.SenderHandle
      ReceiverHandle = entity.ReceiverHandle
      Date = entity.Date
      Message = entity.Message }

let correspondenceItemToEntity threadId (correspondence: CorrespondenceItem) : CorrespondenceItemEntity =
    { Id = Guid.NewGuid().ToString()
      SenderId = correspondence.SenderId
      ReceiverId = correspondence.ReceiverId
      Type = correspondence.Type
      SenderHandle = correspondence.SenderHandle
      ReceiverHandle = correspondence.ReceiverHandle
      Date = correspondence.Date
      Message = correspondence.Message
      ThreadId = threadId }

let getEntityItems (id : string) : seq<CorrespondenceItem> = 
    Log.Information("Contacting DynamoDB for correspondence items with ThreadId: {id}", id)
    context.Scan<CorrespondenceItemEntity>(new ScanCondition("ThreadId", ScanOperator.Equal, id))
    |> Seq.cast
    |> Seq.map entityToCorrespondenceItem

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

let createThread(correspondence : CorrespondenceItem[]) =
    let newId = Guid.NewGuid().ToString()
     
    Log.Information("Creating thread batch write")
    let threadBatch = context.CreateBatchWrite<ThreadEntity>()
    threadBatch.AddPutItem({ ThreadEntity.Id = newId })

    Log.Information("Creating correspondence batch write")
    let correspondenceBatch = context.CreateBatchWrite<CorrespondenceItemEntity>()
    correspondenceBatch.AddPutItems(correspondence |> Seq.map (correspondenceItemToEntity newId) )
    
    Log.Information("Creating whole batch write")
    let batchWrite = new MultiTableBatchWrite(threadBatch, correspondenceBatch)
    
    Log.Information("Executing batch write")
    batchWrite.Execute()

    let thread = context.Load<ThreadEntity>(newId)
    if box thread = null then None
        else Some (newId)
