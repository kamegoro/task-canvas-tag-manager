namespace task_canvas_tag_manager.Driver

open Dapper.FSharp.PostgreSQL
open System.Data

module TaskCanvasDb =
    type Tag = { id: string; name: string }

    let tagTable = table'<Tag> "tag" |> inSchema "task_canvas"

    let selectTags (conn: IDbConnection) : Async<Tag list> =
        async {
            let! queryResult =
                select {
                    for t in tagTable do
                        selectAll
                }
                |> conn.SelectAsync<Tag>
                |> Async.AwaitTask

            return queryResult |> List.ofSeq
        }