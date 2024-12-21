namespace task_canvas_tag_manager.Gateway

open FSharpPlus
open task_canvas_tag_manager.Driver
open task_canvas_tag_manager.Domain
open System.Data
open System

module TagGateway =
    let 全てのタグの取得 (conn: IDbConnection) : タグ.Port.全てのタグの取得 =
        fun () ->
            let データベースのタグ一覧 = TaskCanvasDb.selectTags conn

            データベースのタグ一覧
            |>> List.filter (fun データベースのタグ -> データベースのタグ.is_deleted = false)
            |>> List.map (fun データベースのタグ ->
                { タグ番号 = データベースのタグ.id |> fun v -> タグ番号 v
                  名前 = データベースのタグ.name |> fun v -> タグ名 v })

    let タグの登録 (conn: IDbConnection) : タグ.Port.タグの登録 =
        fun (タグ: タグ) ->
            let データベースのタグ: TaskCanvasDb.Tag =
                { id = タグ.タグ番号 |> fun (タグ番号 v) -> v
                  name = タグ.名前 |> fun (タグ名 v) -> v
                  is_deleted = false }

            データベースのタグ |> TaskCanvasDb.insertTag conn

    let タグの更新 (conn: IDbConnection) : タグ.Port.タグの更新 =
        fun (タグ: タグ) ->
            let データベースのタグ: TaskCanvasDb.Tag =
                { id = タグ.タグ番号 |> fun (タグ番号 v) -> v
                  name = タグ.名前 |> fun (タグ名 v) -> v
                  is_deleted = false }

            データベースのタグ |> TaskCanvasDb.updateTag conn

    let タグの削除 (conn: IDbConnection) : タグ.Port.タグの削除 =
        fun (タグ番号': タグ番号) ->
            let データベースのタグ番号: Guid = タグ番号' |> fun (タグ番号 v) -> v

            TaskCanvasDb.selectTagById conn データベースのタグ番号
            |>> (fun (データベースのタグ: TaskCanvasDb.Tag) -> { データベースのタグ with is_deleted = true })
            >>= TaskCanvasDb.updateTag conn

    let タグの検索 (conn: IDbConnection) : タグ.Port.タグの検索 =
        fun (タグ名': タグ名) ->
            let データベースのタグの変換 (タグ: TaskCanvasDb.Tag) =
                { タグ番号 = タグ.id |> fun v -> タグ番号 v
                  名前 = タグ.name |> fun v -> タグ名 v }

            TaskCanvasDb.searchTags conn (タグ名' |> fun (タグ名 v) -> v)
            |>> (function
            | Ok タグリスト -> タグリスト |> List.map データベースのタグの変換
            | Error ex -> raise ex)
