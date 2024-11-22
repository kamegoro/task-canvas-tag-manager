namespace task_canvas_tag_manager.Domain

open FsUnit
open NUnit.Framework
open System

module タグの履歴作成のテスト =
    [<Test>]
    let タグの履歴作成の成功 () =
        let mutable タグの履歴作成called = 0

        let タグの更新履歴' =
            { 履歴番号 = タグの更新履歴番号 (Guid.NewGuid())
              作成日時 = タグの更新履歴の作成日時 (DateTimeOffset.Now.ToUniversalTime())
              タグ名 = タグ名 "タグ1"
              タグ番号 = タグ番号 (Guid.NewGuid()) }

        let deps: タグの更新履歴 -> Async<unit> =
            fun _ ->
                async {
                    タグの履歴作成called <- タグの履歴作成called + 1
                    return ()
                }

        let actual = タグの更新履歴.登録 deps タグの更新履歴' |> Async.RunSynchronously
        let expected = ()

        actual |> should equal expected
        タグの履歴作成called |> should equal 1
