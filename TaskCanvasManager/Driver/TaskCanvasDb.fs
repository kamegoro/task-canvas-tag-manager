namespace task_canvas_tag_manager.Driver

open Dapper.FSharp.PostgreSQL
open System.Data
open Dapper
open System

module TaskCanvasDb =
    type Tag = { id: Guid; name: string }

    type TagHistory =
        {
            id: Guid;
            tag_id: Guid;
            name: string;
            created_at: DateTimeOffset;
        }

    let tagTable = table'<Tag> "tag" |> inSchema "task_canvas"
    let tagHistoryTable = table'<TagHistory> "tag_history" |> inSchema "task_canvas"

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

    let deleteTag (conn: IDbConnection) (id: Guid) : Async<unit> =
        async {
            return!
                delete {
                    for t in tagTable do
                        where (t.id = id)
                }
                |> conn.DeleteAsync
                |> Async.AwaitTask
                |> Async.Ignore
        }

    let searchTags (conn: IDbConnection) (name: string) : Async<Result<Tag list, exn>> =
        async {
            try
                let query = "SELECT * FROM task_canvas.tag WHERE name LIKE @Name"
                let parameters = {| Name = sprintf "%%%s%%" name |}

                let! result = conn.QueryAsync<Tag>(query, parameters) |> Async.AwaitTask

                return Ok(result |> List.ofSeq)
            with ex ->
                return Error ex
        }

    let insertTagHistory (conn: IDbConnection) (tagHistory: TagHistory) : Async<unit> =
        async {
            return!
                insert {
                    into tagHistoryTable
                    value tagHistory
                }
                |> conn.InsertAsync
                |> Async.AwaitTask
                |> Async.Ignore
        }