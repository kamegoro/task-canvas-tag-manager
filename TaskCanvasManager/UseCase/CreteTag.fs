namespace task_canvas_tag_manager.UseCase

open task_canvas_tag_manager.Domain
open FSharpPlus

module タグの登録 =
    type Deps =
        { タグの登録: タグ.Port.タグの登録
          タグの更新履歴の作成: タグの更新履歴.Port.タグの更新履歴の登録 }

    let 実行 (deps: Deps) (名前: タグ名) : Async<unit> =
        let タグ' = タグ.タグの作成 名前

        let タグの登録 (タグ'': タグ) : Async<タグ> = タグ.登録 deps.タグの登録 タグ'' |>> fun _ -> タグ''

        let タグの更新履歴の登録 (タグ'': タグ) : Async<unit> =
            タグの更新履歴.タグの更新履歴の作成 タグ'' |> タグの更新履歴.登録 deps.タグの更新履歴の作成

        タグ' |> タグの登録 >>= タグの更新履歴の登録
