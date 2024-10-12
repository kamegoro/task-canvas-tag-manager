open System
open Npgsql
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Controllers.Systems
open task_canvas_tag_manager.Controller
open task_canvas_tag_manager.Config
open task_canvas_tag_manager.UseCase
open task_canvas_tag_manager.Gateway
open Microsoft.AspNetCore.Http
open System.Threading.Tasks

let config =
    match FsConfig.EnvConfig.Get<Config>() with
    | Ok config -> config
    | Error e -> failwithf "Failed to load config: %A" e

let createTaskCanvasDbConnPool () =
    let builder =
        new NpgsqlConnectionStringBuilder(
            Host = config.TaskCanvasDb.Host,
            Port = config.TaskCanvasDb.Port,
            Username = config.TaskCanvasDb.Username,
            Database = config.TaskCanvasDb.Database,
            Password = config.TaskCanvasDb.Password
        )

    NpgsqlDataSource.Create(builder.ConnectionString)



[<EntryPoint>]
let main args =

    let builder = WebApplication.CreateBuilder(args)

    builder.Services.AddControllers() |> ignore

    let app = builder.Build()

    let taskCanvasDbDataSource = createTaskCanvasDbConnPool ()

    app.UseHttpsRedirection() |> ignore

    app.UseAuthorization() |> ignore

    app.MapControllers() |> ignore

    app.MapGet("/v1/systems/ping", Func<IResult> ping) |> ignore

    app.MapGet(
        "/v1/tags",
        Func<IResult>(fun _ ->
            let deps: 全てのタグの取得.Deps =
                { 全てのタグの取得 = TagGateway.全てのタグの取得 (taskCanvasDbDataSource.CreateConnection()) }

            let getTags = GetTags.controller deps

            getTags |> Async.RunSynchronously)
    )
    |> ignore

    app.Run("http://localhost:9090")

    0
