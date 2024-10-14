namespace task_canvas_tag_manager.Port

open task_canvas_tag_manager.Domain

type 全てのタグの取得 = (unit) -> Async<タグ list>

type タグの登録 = タグ -> Async<unit>

type タグの更新 = タグ -> Async<unit>

type タグの削除 = タグ番号 -> Async<unit>