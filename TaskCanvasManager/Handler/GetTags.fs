namespace task_canvas_tag_manager.Handler

open FSharpPlus

open Microsoft.AspNetCore.Http
open task_canvas_tag_manager.UseCase
open task_canvas_tag_manager.Domain

module GetTags =
    type Tag = { id: string; name: string }
    type ResponseJson = { tags: Tag list }

    let toJson (tags: タグ list) : ResponseJson =
        tags
        |> List.map (fun tag ->
            { id = tag.タグ番号 |> fun (タグ番号 v) -> v.ToString()
              name = tag.名前 |> fun (タグ名 v) -> v })
        |> fun tags -> { tags = tags }

    let handler (deps: 全てのタグの取得.Deps) : Async<IResult> =
        全てのタグの取得.実行 deps |> Async.map toJson |> Async.map (fun json -> Results.Ok json)
