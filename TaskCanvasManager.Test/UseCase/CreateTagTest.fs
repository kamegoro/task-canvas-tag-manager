namespace task_canvas_tag_manager.UseCase

open FsUnit
open NUnit.Framework
open task_canvas_tag_manager.Domain

module タグの登録のテスト =
    [<Test>]
    let タグの新規登録がされる () =
        let mutable タグの登録called = 0
        let mutable タグの更新履歴の作成called = 0

        let タグ名 = タグ名 "タグ1"
        let タグ = タグ.タグの作成 タグ名
        let タグの更新履歴 = タグの更新履歴.タグの更新履歴の作成 タグ

        let deps: タグの登録.Deps =
            {
                タグの登録 =
                    fun (タグ: タグ) ->
                        async {
                            match タグ with
                            | { 名前 = タグ名 "タグ1" } -> タグの登録called <- タグの登録called + 1
                            | _ -> ()

                            return ()
                        }
                タグの更新履歴の作成 =
                    fun (タグの更新履歴: タグの更新履歴) ->
                        async {
                            if タグの更新履歴 = タグの更新履歴 then
                                タグの更新履歴の作成called <- タグの更新履歴の作成called + 1

                            return ()
                        }
            }

        タグの登録.実行 deps タグ名 |> Async.RunSynchronously

        タグの登録called |> should equal 1
        タグの更新履歴の作成called |> should equal 1
