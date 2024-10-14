namespace task_canvas_tag_manager.UseCase

open task_canvas_tag_manager.Port
open task_canvas_tag_manager.Domain


module タグの更新 =
    type Deps = { タグの更新: タグの更新 }

    let 実行 (deps: Deps) (タグ: タグ) : Async<unit> =
        deps.タグの更新 タグ