namespace task_canvas_tag_manager.Handler

open System
open FSharpPlus
open Microsoft.AspNetCore.Http
open task_canvas_tag_manager.UseCase
open task_canvas_tag_manager.Domain

module UpdateTag =
    type UpdateTagRequestJson = {
        name: string;
    }

    type Tag = {
        id: string;
        name: string;
    }

    let handler (deps: タグの更新.Deps) (タグ: Tag) : Async<IResult> =
        try
            let タグ : タグ = {
                タグ番号 = タグ番号 (Guid.Parse タグ.id);
                名前 = タグ名 タグ.name;
            }

            タグの更新.実行 deps タグ
            |> Async.map (fun _ -> Results.Ok())
        with ex ->
            async { return Results.Problem(ex.Message) }