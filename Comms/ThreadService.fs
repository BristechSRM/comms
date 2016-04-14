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

let correspondenceToEntity threadId (correspondence: Correspondence) : CorrespondenceEntity =
    let entity = new CorrespondenceEntity()
    entity.Id <- Guid.NewGuid().ToString()
    entity.SenderId <- correspondence.SenderId
    entity.ReceiverId <- correspondence.ReceiverId
    entity.Type <- correspondence.Type
    entity.SenderHandle <- correspondence.SenderHandle
    entity.ReceiverHandle <- correspondence.ReceiverHandle
    entity.Date <- correspondence.Date
    entity.Message <- correspondence.Message
    entity.ThreadId <- threadId
    entity

let getEntityItems (id : string) : seq<Correspondence> = 
    Log.Information("Contacting DynamoDB for correspondence items with ThreadId: {id}", id)
    let x = context.Scan<CorrespondenceEntity>(new ScanCondition("ThreadId", ScanOperator.Equal, id))
    x
    |> Seq.cast
    |> Seq.map entityToCorrespondence

let entityToThread (entity : ThreadEntity) : ThreadDetail = 
    Log.Information("{S}", entity)
    { Id = entity.Id
      Items = entity.Id |> getEntityItems }


let getThread (id : string) = 
    Log.Information("Contacting DynamoDB for thread with Id: {id}", id)
    let threads = context.Scan<ThreadEntity>(new ScanCondition("Id", ScanOperator.Equal, id))
    let threadArray =  Seq.toArray threads

    if Seq.isEmpty threadArray then None
        else Some(entityToThread (Seq.head threadArray))
        

let getThreads() = 
    Log.Information("Contacting DynamoDB for threads")
    context.Scan<ThreadEntity>() |> Seq.map entityToThread

let createCorrespondence threadId correspondence =
    Log.Information "Create correspondence entry in Dynamo DB"
    let entity = correspondenceToEntity threadId correspondence
    let newId = entity.Id
    context.Save entity
    newId

let createThread(correspondence : Correspondence[]) =
    Log.Information("Creating entry in DynamoDB")
    let newId = Guid.NewGuid().ToString()
    let newThread = new ThreadEntity()
    newThread.Id <- newId
    context.Save newThread

    let correspondenceBatch = context.CreateBatchWrite<CorrespondenceEntity>()
    correspondenceBatch.AddPutItems(correspondence |> Seq.map (correspondenceToEntity newId) )
    correspondenceBatch.Execute()
    
    let threads = context.Scan<ThreadEntity>(new ScanCondition("Id", ScanOperator.Equal, newId))
    let threadArray =  Seq.toArray threads

    if Seq.isEmpty threadArray then None
    else Some(Seq.head threadArray)
