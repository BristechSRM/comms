module Controllers

open System.Collections.Generic
open System.Web.Http
open Newtonsoft.Json

let lastContactedData = 
    [("david.wybourn@superawesomegoodcode.co.uk", "2016-03-07T12:45:04Z");
     ("chris.smith@leaddeveloper.com", "2016-02-17T15:51:15Z");
     ("bob.builder@cartoonconstructionslimited.tv", "2004-01-30T05:00:01Z");]
    |> Map.ofList

type LastContactedController() =
    inherit ApiController()

    member __.Get() = 
        JsonConvert.SerializeObject(lastContactedData)