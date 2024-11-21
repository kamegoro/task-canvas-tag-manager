namespace task_canvas_tag_manager.Domain

open FsUnit
open NUnit.Framework
open System

module タグのテスト =
    [<Test>]
    let タグの登録の成功 () =
        let mutable タグの登録called = 0

        let タグ' =
            { タグ番号 = タグ番号 (Guid.NewGuid())
              名前 = タグ名 "タグ1" }

        let deps: タグ -> Async<unit> =
            fun _ ->
                async {
                    タグの登録called <- タグの登録called + 1
                    return ()
                }

        let actual = タグ.登録 deps タグ' |> Async.RunSynchronously

        let expected = ()

        actual |> should equal expected
        タグの登録called |> should equal 1
