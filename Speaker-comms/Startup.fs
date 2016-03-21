module Server

open Owin
open System.Web.Http
open Newtonsoft.Json.Serialization

type Config = {
    id : RouteParameter
}

let setRoutes (config : HttpConfiguration) =
    config.Routes.MapHttpRoute("DefaultApi", "{controller}/{id}", { id = RouteParameter.Optional }) |> ignore
    config

let setFormatters (config: HttpConfiguration) = 
    config.Formatters.Remove config.Formatters.XmlFormatter |> ignore
    config.Formatters.JsonFormatter.SerializerSettings.ContractResolver <- DefaultContractResolver()

let createConfig = 
    let config = new HttpConfiguration()
    config |> setRoutes |> setFormatters
    config

type Startup() = 
    member this.Configuration(app: IAppBuilder) = 
        let config = createConfig
        app.UseWebApi config |> ignore