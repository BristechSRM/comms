namespace SpeakerComms.LastContactedController

open System.Web.Http
open SpeakerComms.LastContactedService
open System.Net.Http
open Serilog

[<Route("last-contacted")>]
type LastContactedController() =
    inherit ApiController()

    member x.Get() = 
        Log.Information("Received GET request for last contacted")
        x.Request.CreateResponse(getLastContacted())
