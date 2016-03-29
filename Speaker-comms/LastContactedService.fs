module Speakercomms.LastContactedService

open Speakercomms.CorrespondenceService
open System
open Speakercomms.Models

let updateMap key value (map : Map<String, String>) = 
    if not <| map.ContainsKey(key) || ((Map.find key map) < value)then
        map.Add (key, value)
    else 
        map

let getLastContacted () =                     
    let items = getCorrespondence()
    let mutable lastContactedMap : Map<String, String> = Map.empty
        
    for item in items do
        lastContactedMap <- 
            lastContactedMap
            |> updateMap item.From item.Date
            |> updateMap item.To item.Date
        
    lastContactedMap