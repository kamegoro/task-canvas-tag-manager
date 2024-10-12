namespace task_canvas_tag_manager.Port

open task_canvas_tag_manager.Domain

type 全てのタグの取得 = (unit) -> Async<タグ list>
