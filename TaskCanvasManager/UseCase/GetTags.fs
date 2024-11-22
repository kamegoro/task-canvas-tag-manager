namespace task_canvas_tag_manager.UseCase

open task_canvas_tag_manager.Domain

module 全てのタグの取得 =
    type Deps = { 全てのタグの取得: タグ.Port.全てのタグの取得 }

    let 実行 (deps: Deps) : Async<タグ list> = deps.全てのタグの取得 ()
