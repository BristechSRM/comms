namespace SpeakerComms.LastContactedController

open System.Web.Http
open SpeakerComms.LastContactedService
open System.Net.Http

type LastContactedController() =
    inherit ApiController()

    member x.Get() = 
        x.Request.CreateResponse(getLastContacted())
