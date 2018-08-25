// Learn more about F# at http://fsharp.org

open EasyNetQ
open EasyNetQ
open Microsoft.Extensions.Hosting
open System
open System.Threading.Tasks
open Microsoft.Extensions.Logging
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Fib.Common.Messages
type RabbitService(bus: IBus, logger: ILogger<RabbitService>) =
    interface IHostedService with
        member this.StartAsync(ct) =
            bus.Subscribe<FibCalculated>("TestFsharp", fun msg -> logger.LogInformation(sprintf "Received msg %s" (msg.ToString()))) |> ignore
            Task.CompletedTask
        member this.StopAsync(ct) = Task.CompletedTask

[<EntryPoint>]
let main argv =
    let builder = HostBuilder()
                    .ConfigureAppConfiguration(fun context config ->
                                        config.AddJsonFile("appsettings.json", optional = true) |> ignore
                                        config.AddEnvironmentVariables() |> ignore
                                        if argv |> isNull |> not then
                                          config.AddCommandLine(argv) |> ignore
                                      )
                    .ConfigureServices(fun ct services ->
                                          services.AddOptions() |> ignore
                                          services.AddSingleton<IBus>(fun _ -> RabbitHutch.CreateBus("host=localhost;username=guest;password=guest")) |> ignore
                                          services.AddSingleton<IHostedService, RabbitService>() |> ignore
                                        )
                    .ConfigureLogging(fun ctx logging ->
                                        logging.AddConfiguration(ctx.Configuration.GetSection("Logging")) |> ignore
                                        logging.AddConsole() |> ignore
                                        )
    let host = builder.Build()
    host.RunAsync() |> Async.AwaitTask |> Async.RunSynchronously
    0
    
