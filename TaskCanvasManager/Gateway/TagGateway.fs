namespace task_canvas_tag_manager.Gateway

open task_canvas_tag_manager.Driver
open task_canvas_tag_manager.Domain
open task_canvas_tag_manager.Port
open System.Data
open System

module TagGateway =
    let 全てのタグの取得 (conn: IDbConnection) : 全てのタグの取得 =
        fun () ->
            async {
                let! データベースのタグ一覧 = TaskCanvasDb.selectTags conn

                let タグ一覧 =
                    データベースのタグ一覧
                    |> List.filter (fun データベースのタグ -> データベースのタグ.is_deleted = false)
                    |> List.map (fun タグ -> { タグ番号 = タグ番号 タグ.id; 名前 = タグ名 タグ.name })

                return タグ一覧
            }

    let タグの登録 (conn: IDbConnection) : タグの登録 =
        fun (タグ: タグ) ->
            async {
                let データベースのタグ: TaskCanvasDb.Tag =
                    { id = タグ.タグ番号 |> fun (タグ番号 v) -> v
                      name = タグ.名前 |> fun (タグ名 v) -> v
                      is_deleted = false }

                return! TaskCanvasDb.insertTag conn データベースのタグ
            }

    let タグの更新 (conn: IDbConnection) : タグの更新 =
        fun (タグ: タグ) ->
            async {
                let データベースのタグ: TaskCanvasDb.Tag =
                    { id = タグ.タグ番号 |> fun (タグ番号 v) -> v
                      name = タグ.名前 |> fun (タグ名 v) -> v
                      is_deleted = false }

                return! TaskCanvasDb.updateTag conn データベースのタグ
            }

    let タグの削除 (conn: IDbConnection) : タグの削除 =
        fun (タグ番号': タグ番号) ->
            async {
                let データベースのタグ番号: Guid = タグ番号' |> fun (タグ番号 v) -> v

                let! データベースのタグ = TaskCanvasDb.selectTagById conn データベースのタグ番号

                return! TaskCanvasDb.updateTag conn { データベースのタグ with is_deleted = true }
            }

    let タグの検索 (conn: IDbConnection) : タグの検索 =
        fun (タグ名': タグ名) ->
            async {
                let データベースのタグの変換 (タグ: TaskCanvasDb.Tag) =
                    { タグ番号 = タグ.id |> fun v -> タグ番号 v
                      名前 = タグ.name |> fun v -> タグ名 v }

                let! データベースのタグの検索結果 = TaskCanvasDb.searchTags conn (タグ名' |> fun (タグ名 v) -> v)

                match データベースのタグの検索結果 with
                | Ok タグリスト ->
                    let タグ一覧 = タグリスト |> List.map データベースのタグの変換
                    return タグ一覧
                | Error ex ->
                    // Handle the error case appropriately
                    return raise ex
            }
