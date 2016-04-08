namespace Comms.LastContactController

open System.Web.Http
open Comms.LastContactService
open System.Net.Http
open Serilog

[<Route("last-contact")>]
type LastContact() =
    inherit ApiController()

    member x.Get() =
        Log.Information("Received GET request for last contact")
        x.Request.CreateResponse(getLastContact())
