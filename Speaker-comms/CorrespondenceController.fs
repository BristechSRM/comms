namespace SpeakerComms.CorrespondenceController

open System.Web.Http
open SpeakerComms.CorrespondenceService

type CorrespondenceController() = 
    inherit ApiController()

    member __.Get() = 
        getCorrespondence()
