namespace Comms.LastContactedController

open System.Web.Http
open Comms.LastContactedService
open System.Net.Http
open Serilog

[<Route("last-contact")>]
type LastContactedController() =
    inherit ApiController()

    member x.Get() =
        Log.Information("Received GET request for last contacted")
        x.Request.CreateResponse(getLastContacted())
