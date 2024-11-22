namespace task_canvas_tag_manager.Domain

open FsUnit
open NUnit.Framework
open System

module タグのテスト =
    module タグの登録のテスト =
        [<Test>]
        let タグの登録の成功 () =
            let mutable タグの登録called = 0

            let タグ' =
                { タグ番号 = タグ番号 (Guid.NewGuid())
                  名前 = タグ名 "タグ1" }

            let deps: タグ.Port.タグの登録 =
                fun _ ->
                    async {
                        タグの登録called <- タグの登録called + 1
                        return ()
                    }

            let actual = タグ.登録 deps タグ' |> Async.RunSynchronously

            let expected = ()

            actual |> should equal expected
            タグの登録called |> should equal 1

    module タグの更新のテスト =
        [<Test>]
        let タグの更新の成功 () =
            let mutable タグの更新called = 0

            let タグ' =
                { タグ番号 = タグ番号 (Guid.NewGuid())
                  名前 = タグ名 "タグ1" }

            let deps: タグ.Port.タグの更新 =
                fun _ ->
                    async {
                        タグの更新called <- タグの更新called + 1
                        return ()
                    }

            let actual = タグ.更新 deps タグ' |> Async.RunSynchronously

            let expected = ()

            actual |> should equal expected
            タグの更新called |> should equal 1

    module タグの削除のテスト =
        [<Test>]
        let タグの削除の成功 () =
            let mutable タグの削除called = 0

            let タグ番号' = タグ番号 (Guid.NewGuid())

            let deps: タグ.Port.タグの削除 =
                fun _ ->
                    async {
                        タグの削除called <- タグの削除called + 1
                        return ()
                    }

            let actual = タグ.削除 deps タグ番号' |> Async.RunSynchronously

            let expected = ()

            actual |> should equal expected
            タグの削除called |> should equal 1


    module タグの全権取得のテスト =
        [<Test>]
        let タグの全権取得の成功 () =
            let タグ' =
                { タグ番号 = タグ番号 (Guid.NewGuid())
                  名前 = タグ名 "タグ1" }

            let mutable タグの全権取得called = 0

            let deps: タグ.Port.全てのタグの取得 =
                fun _ ->
                    async {
                        タグの全権取得called <- タグの全権取得called + 1

                        return [ タグ' ]
                    }

            let actual = タグ.全件取得 deps |> Async.RunSynchronously

            let expected = [ タグ' ]

            actual |> should equal expected
            タグの全権取得called |> should equal 1
