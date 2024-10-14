open System
open Npgsql
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Controllers.Systems
open task_canvas_tag_manager.Handler
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
        Func<Task<IResult>>(fun _ ->
            let deps: 全てのタグの取得.Deps =
                { 全てのタグの取得 = TagGateway.全てのタグの取得 (taskCanvasDbDataSource.CreateConnection()) }

            let getTags = GetTags.handler deps

            getTags |> Async.StartAsTask)
    )
    |> ignore

    app.MapPost(
        "/v1/tags",
        Func<CreateTag.TagRequestJson, Task<IResult>>(fun req ->
            let deps: タグの登録.Deps =
                { タグの登録 = TagGateway.タグの登録 (taskCanvasDbDataSource.CreateConnection()) }

            let registerTag = CreateTag.handler deps req.name

            registerTag |> Async.StartAsTask)
    )
    |> ignore

    app.MapPut(
        "/v1/tags/{id}",
        Func<string, UpdateTag.UpdateTagRequestJson, Task<IResult>>(fun id req ->
            let deps: タグの更新.Deps =
                { タグの更新 = TagGateway.タグの更新 (taskCanvasDbDataSource.CreateConnection()) }

            let updateTag = UpdateTag.handler deps { id = id; name = req.name }

            updateTag |> Async.StartAsTask)
    )
    |> ignore

    app.Run("http://localhost:9090")

    0
