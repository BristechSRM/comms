namespace SpeakerComms.CorrespondenceController

open System.Web.Http
open SpeakerComms.CorrespondenceService
open System.Net.Http

type CorrespondenceController() = 
    inherit ApiController()

    member x.Get() = 
        x.Request.CreateResponse(getCorrespondence())
