module Comms.Models

type CorrespondenceItem = {
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
    Items : seq<CorrespondenceItem>
}

type LastContactSummary = {
    ThreadId : string
    Date : string
    SenderId : string
    ReceiverId : string
}