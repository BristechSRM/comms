module Comms.LastContactService

open Comms.ThreadService
open Comms.Models
open System
open System.Collections.Generic
open Serilog

let threadToLastContactSummary(thread : ThreadDetail): LastContactSummary =
    let lastCorrespondence = thread.Items |> Seq.maxBy (fun c -> c.Date)
    {
        ThreadId = thread.Id
        Date = lastCorrespondence.Date
        SenderId = lastCorrespondence.SenderId
        ReceiverId = lastCorrespondence.ReceiverId
    }


let getLastContact () =
    Log.Information("Calculating last contact from correspondence")

    getThreads() |> Seq.map threadToLastContactSummary 
