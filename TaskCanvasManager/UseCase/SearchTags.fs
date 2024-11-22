namespace task_canvas_tag_manager.UseCase

open task_canvas_tag_manager.Domain

module タグの検索 =
    type Deps = { タグの検索: タグ.Port.タグの検索 }

    let 実行 (deps: Deps) (タグ名': タグ名) = deps.タグの検索 タグ名'
