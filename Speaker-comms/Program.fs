module Program

open Microsoft.Owin.Hosting
open SpeakerComms.Startup
open System.Threading

[<EntryPoint>]
let main _ = 
    let baseUrl = "http://*:9001"
    use server = WebApp.Start<Startup>(baseUrl)
    printfn "Running on %s" baseUrl

    let waitIndefinitelyWithToken = 
        let cancelSource = new CancellationTokenSource()
        cancelSource.Token.WaitHandle.WaitOne() |> ignore
    0
