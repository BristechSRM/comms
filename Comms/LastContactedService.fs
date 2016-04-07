module SpeakerComms.LastContactedService

open SpeakerComms.CorrespondenceService
open System
open System.Collections.Generic
open Serilog

let update key value (map : Dictionary<String, String>) = 
    if not <| map.ContainsKey(key) || map.[key] < value then
        map.[key] <- value

let getLastContacted () =
    Log.Information("Calculating last contacted from correspondence items")

    let map = Dictionary<string, string>()
        
    for item in getCorrespondence() do
        map |> update item.From item.Date 
        map |> update item.To item.Date
        
    map