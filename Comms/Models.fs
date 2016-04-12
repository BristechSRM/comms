module Comms.Models

open Amazon.DynamoDBv2.DataModel
open System

type Correspondence = {
    SenderId : string
    ReceiverId : string
    Type : string
    SenderHandle : string
    ReceiverHandle : string
    Date : string
    Message : string
    }

type ThreadDetail = {
    Id : string
    Items : seq<Correspondence>
}

[<DynamoDBTable("Thread")>]
type ThreadEntity()  = 
    member val Id = "" with get, set
    
[<DynamoDBTable("CorrespondenceItem")>]
type CorrespondenceEntity() =
    member val Id = "" with get, set
    member val SenderId = "" with get, set
    member val ReceiverId = "" with get, set
    member val Date = "" with get, set
    member val Message = "" with get, set
    member val Type= "" with get, set
    member val SenderHandle = "" with get, set
    member val ReceiverHandle = "" with get, set
    member val ThreadId = "" with get, set



type LastContactSummary = {
    ThreadId : string
    Date : string
    SenderId : string
    ReceiverId : string
}