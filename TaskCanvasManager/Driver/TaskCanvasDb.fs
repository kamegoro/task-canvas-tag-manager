namespace task_canvas_tag_manager.Driver

open Dapper.FSharp.PostgreSQL
open System.Data
open System

module TaskCanvasDb =
    type Tag = { id: Guid; name: string }

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

    let insertTag (conn: IDbConnection) (タグ: Tag) : Async<unit> =
        async {
            return!
                insert {
                    into tagTable
                    value タグ
                }
                |> conn.InsertAsync
                |> Async.AwaitTask
                |> Async.Ignore
        }

    let updateTag (conn: IDbConnection) (タグ: Tag) : Async<unit> =
        async {
            return!
                update {
                    for t in tagTable do
                    set { id = タグ.id; name = タグ.name }
                    where (t.id = タグ.id)
                }
                |> conn.UpdateAsync
                |> Async.AwaitTask
                |> Async.Ignore
        }