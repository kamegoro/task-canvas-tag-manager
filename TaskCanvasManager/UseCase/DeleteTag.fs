namespace task_canvas_tag_manager.UseCase

open task_canvas_tag_manager.Domain
open task_canvas_tag_manager.Port

module タグの削除 =
    type Deps = { タグの削除: タグの削除 }

    let 実行 (deps: Deps) (タグ番号': タグ番号) : Async<unit> =
        deps.タグの削除 タグ番号'