namespace SpeakerComms.LastContactedController

open System.Web.Http
open SpeakerComms.LastContactedService

type LastContactedController() =
    inherit ApiController()

    member __.Get() = 
        // Temporary, being used just for demo.
        let lastContactedData = 
            ["saatviga.sudhahar@bristol.ac.uk", "2016/03/22";
             "carolyn@kwmc.org.uk", "2016/03/11";
             "matt@doics.co", "2016/03/11";
             "l.kreczko@bristol.ac.uk", "2016/02/29";
             "ddrozdzewski@scottlogic.com", "2016/02/17";
             "me@rachelandrew.co.uk", "2016/02/26";
             "toby@warmfusion.co.uk", "2016/03/11";
             "rich@rich-knight.com", "2016/03/11";
             "tom@ultrahaptics.com", "2016/02/12";
             "12e.david@gmail.com", "2016/02/19";]
             |> Map.ofList
        lastContactedData
        //getLastContacted()