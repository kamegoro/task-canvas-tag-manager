namespace task_canvas_tag_manager.Gateway

open task_canvas_tag_manager.Driver
open task_canvas_tag_manager.Domain
open System.Data

module TagUpdateHistoryGateway =
    let タグの更新履歴の作成 (conn: IDbConnection) : タグの更新履歴.Port.タグの更新履歴の登録 =
        fun (タグの更新履歴: タグの更新履歴) ->
            let データベースのタグの更新履歴: TaskCanvasDb.TagHistory =
                { id = タグの更新履歴.履歴番号 |> fun (タグの更新履歴番号 v) -> v
                  tag_id = タグの更新履歴.タグ番号 |> fun (タグ番号 v) -> v
                  name = タグの更新履歴.タグ名 |> fun (タグ名 v) -> v
                  created_at = タグの更新履歴.作成日時 |> fun (タグの更新履歴の作成日時 v) -> v }

            TaskCanvasDb.insertTagHistory conn データベースのタグの更新履歴
