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
                    データベースのタグ一覧 |> List.map (fun タグ -> { タグ番号 = タグ番号 タグ.id; 名前 = タグ名 タグ.name })

                return タグ一覧
            }

    let タグの登録 (conn: IDbConnection) : タグの登録 =
        fun (タグ: タグ) ->
            async {
                let データベースのタグ: TaskCanvasDb.Tag =
                    { id = タグ.タグ番号 |> fun (タグ番号 v) -> v
                      name = タグ.名前 |> fun (タグ名 v) -> v }

                return! TaskCanvasDb.insertTag conn データベースのタグ
            }

    let タグの更新 (conn: IDbConnection) : タグの更新 =
        fun (タグ: タグ) ->
            async {
                let データベースのタグ: TaskCanvasDb.Tag =
                    { id = タグ.タグ番号 |> fun (タグ番号 v) -> v
                      name = タグ.名前 |> fun (タグ名 v) -> v }

                return! TaskCanvasDb.updateTag conn データベースのタグ
            }

    let タグの削除 (conn: IDbConnection) : タグの削除 =
        fun (タグ番号': タグ番号) ->
            async {
                let データベースのタグ番号: Guid = タグ番号' |> fun (タグ番号 v) -> v

                return! TaskCanvasDb.deleteTag conn データベースのタグ番号
            }
