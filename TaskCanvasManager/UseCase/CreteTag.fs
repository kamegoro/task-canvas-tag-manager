namespace task_canvas_tag_manager.UseCase

open task_canvas_tag_manager.Domain

module タグの登録 =
    type Deps =
        {
            タグの登録: タグ -> Async<unit>
            タグの更新履歴の作成: タグの更新履歴 -> Async<unit>
        }

    let 実行 (deps: Deps) (名前: タグ名) : Async<unit> =
        async {
            let タグ = タグ.タグの作成 名前

            do! deps.タグの登録 タグ

            let タグの更新履歴 = タグの更新履歴.タグの更新履歴の作成 タグ

            do! deps.タグの更新履歴の作成 タグの更新履歴
        }
