module Comms.ExternalIdsController

open Comms.CorrespondenceService
open Serilog
open System.Net
open System.Net.Http
open System.Web.Http

type ExternalIdsController() = 
    inherit ApiController()
    
    member x.Get(handle : string) = 
        Log.Information("Received get request for external ids connected with handle: {handle}", handle)
        let ids = getExternalIdsByHandle handle
        x.Request.CreateResponse(ids)