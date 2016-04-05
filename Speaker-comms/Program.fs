module Program

open Microsoft.Owin.Hosting
open SpeakerComms.Startup
open System.Threading

(*
    Note: When running this app from Visual studio / On Windows / Possibly with mono develop (Not checked)
    Because of its use of the network interfaces, you'll need to run Visual studio as administrator.
    However the better solution is to do the following:

    Open a command prompt as administrator and run the following command replacing username with your username
    (The port needs to match the port the service will run on)
    netsh http ad urlacl url=http://*:8080/ user=username

    After running this command, you won't need to run visual studio as administrator again.

    Reference : http://stackoverflow.com/questions/27842979/owin-webapp-start-gives-a-first-chance-exception-of-type-system-reflection-targ 
*)

[<EntryPoint>]
let main _ = 
    let baseUrl = "http://*:8080"
    WebApp.Start<Startup>(baseUrl) |> ignore
    printfn "Running on %s" baseUrl

    let cancelSource = new CancellationTokenSource()
    cancelSource.Token.WaitHandle.WaitOne() |> ignore
    0
