namespace task_canvas_tag_manager.UseCase

open task_canvas_tag_manager.Domain
open FsUnit
open NUnit.Framework
open System

module タグの検索のテスト =
    [<Test>]
    let タグの検索の成功 () =
        let mutable タグの検索の実行called = 0

        let タグの検索結果: タグ list =
            [ { タグ番号 = タグ番号 (Guid.NewGuid())
                名前 = タグ名 "タグ1" }
              { タグ番号 = タグ番号 (Guid.NewGuid())
                名前 = タグ名 "タグ2" } ]

        let deps: タグの検索.Deps =
            { タグの検索 =
                fun タグ名' ->
                    async {
                        match タグ名' with
                        | タグ名 "タグ1" ->
                            タグの検索の実行called <- タグの検索の実行called + 1
                            return タグの検索結果
                        | _ -> return []
                    } }

        タグの検索.実行 deps (タグ名 "タグ1") |> Async.RunSynchronously |> should equal タグの検索結果

        タグの検索の実行called |> should equal 1
