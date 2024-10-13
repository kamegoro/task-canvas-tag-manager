namespace task_canvas_tag_manager.UseCase

open task_canvas_tag_manager.Domain

module タグの登録 =
    type Deps = { タグの登録: タグ -> Async<unit> }

    let 実行 (deps: Deps) (名前: タグ名) : Async<unit> =
        let タグ = タグ.タグの作成 名前

        deps.タグの登録 タグ