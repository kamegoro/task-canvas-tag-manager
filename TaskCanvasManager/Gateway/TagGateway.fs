namespace task_canvas_tag_manager.Gateway

open task_canvas_tag_manager.Driver
open task_canvas_tag_manager.Domain
open task_canvas_tag_manager.Port
open System.Data

module TagGateway =
    let 全てのタグの取得 (conn: IDbConnection) : 全てのタグの取得 =
        fun () ->
            async {
                let! データベースのタグ一覧 = TaskCanvasDb.selectTags conn

                let タグ一覧 =
                    データベースのタグ一覧 |> List.map (fun タグ -> { タグ番号 = タグ番号 (タグ.id.ToString()); 名前 = タグ名 タグ.name })

                return タグ一覧
            }
