module Comms.Models

open System
open Comms.Entities

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

type LastContactSummary = {
    ThreadId : string
    Date : string
    SenderId : string
    ReceiverId : string
}