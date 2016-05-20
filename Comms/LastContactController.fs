module LastContactController

open LastContactService
open Serilog
open System.Net.Http
open System.Web.Http

[<Route("last-contact")>]
type LastContactController() = 
    inherit ApiController()
    member x.Get() = 
        Log.Information("Received GET request for last contact")
        x.Request.CreateResponse(getAllLastContacts())
