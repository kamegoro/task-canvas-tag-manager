namespace task_canvas_tag_manager.Handler

open FSharpPlus
open task_canvas_tag_manager.UseCase
open task_canvas_tag_manager.Domain
open System
open Microsoft.AspNetCore.Http

module SearchTags =
    type Tag = { id: Guid; name: string }

    let toJson (tags: タグ list) : Tag list =
        tags
        |> List.map (fun タグ ->
            { id = タグ.タグ番号 |> fun (タグ番号 v) -> v
              name = タグ.名前 |> fun (タグ名 v) -> v })

    let handler (deps: タグの検索.Deps) (name: string) : Async<IResult> =
        try
            タグの検索.実行 deps (タグ名 name)
            |> Async.RunSynchronously
            |> toJson
            |> fun json -> async { return Results.Ok(json) }
        with exn ->
            async { return Results.Problem(exn.Message) }
