namespace task_canvas_tag_manager.Handler

open FSharpPlus
open task_canvas_tag_manager.UseCase
open task_canvas_tag_manager.Domain
open System
open Microsoft.AspNetCore.Http

module DeleteTag =
    let handler (deps: タグの削除.Deps) (reqTagId: Guid) : Async<IResult> =
        try
            タグの削除.実行 deps (タグ番号 reqTagId)
            |> Async.map (fun _ -> Results.Ok())

        with ex ->
            async { return Results.Problem(ex.Message) }