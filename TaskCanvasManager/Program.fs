open System
open Npgsql
open Microsoft.AspNetCore.Builder
open Controllers.Systems
open task_canvas_tag_manager.Handler
open task_canvas_tag_manager.Config
open task_canvas_tag_manager.UseCase
open task_canvas_tag_manager.Gateway
open Microsoft.AspNetCore.Http
open System.Threading.Tasks
open System.Text.RegularExpressions

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

let isJapanese (input: string) : bool =
    Regex.IsMatch(input, @"[\p{IsHiragana}\p{IsKatakana}\p{IsCJKUnifiedIdeographs}]")

[<EntryPoint>]
let main args =

    let builder = WebApplication.CreateBuilder(args)

    let taskCanvasDbDataSource = createTaskCanvasDbConnPool ()

    let app = builder.Build()

    app.MapGet("/v1/systems/ping", Func<IResult> ping) |> ignore

    app.MapGet(
        "/v1/tags",
        Func<HttpContext, Task<IResult>>(fun ctx ->
            async {
                try
                    let queryParams = ctx.Request.Query

                    let getDeps: 全てのタグの取得.Deps =
                        { 全てのタグの取得 = TagGateway.全てのタグの取得 (taskCanvasDbDataSource.CreateConnection()) }

                    let searchDeps: タグの検索.Deps =
                        { タグの検索 = TagGateway.タグの検索 (taskCanvasDbDataSource.CreateConnection()) }

                    let nameOption: string option =
                        match queryParams.TryGetValue("name") with
                        | true, name when not (String.IsNullOrWhiteSpace(name)) -> Some(name.ToString())
                        | _ -> None

                    printfn "Name Option: %A" nameOption

                    let searchTags: Async<IResult> =
                        match nameOption with
                        | Some n when isJapanese n -> SearchTags.handler searchDeps n
                        | Some n -> SearchTags.handler searchDeps n
                        | None -> GetTags.handler getDeps

                    return! searchTags
                with ex ->
                    return Results.Problem("An error occurred while processing the request.")
            }
            |> Async.StartAsTask)
    )
    |> ignore

    app.MapPost(
        "/v1/tags",
        Func<CreateTag.TagRequestJson, Task<IResult>>(fun req ->
            let deps: タグの登録.Deps =
                { タグの登録 = TagGateway.タグの登録 (taskCanvasDbDataSource.CreateConnection())
                  タグの更新履歴の作成 = TagUpdateHistoryGateway.タグの更新履歴の作成 (taskCanvasDbDataSource.CreateConnection()) }

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

    app.MapDelete(
        "/v1/tags/{id}",
        Func<string, Task<IResult>>(fun id ->
            let deps: タグの削除.Deps =
                { タグの削除 = TagGateway.タグの削除 (taskCanvasDbDataSource.CreateConnection()) }

            let deleteTag = DeleteTag.handler deps (Guid.Parse(id))

            deleteTag |> Async.StartAsTask)
    )
    |> ignore


    app.Run("http://localhost:9090")

    0
