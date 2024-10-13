namespace task_canvas_tag_manager.UseCase

open FsUnit
open NUnit.Framework
open task_canvas_tag_manager.Domain

module タグの登録のテスト =
    [<Test>]
    let タグの新規登録がされる () =
      let mutable タグの登録called = 0

      let deps : タグの登録.Deps = { タグの登録 = fun (タグ: タグ) ->
        async {
          match タグ with
          | { 名前 = タグ名 "タグ1" } -> タグの登録called <- タグの登録called + 1
          | _ -> ()

          return ()
        }}

      let タグ = タグ.タグの作成 (タグ名 "タグ1")

      タグの登録.実行 deps (タグ名 "タグ1") |> Async.RunSynchronously

      タグの登録called |> should equal 1