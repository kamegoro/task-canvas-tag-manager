namespace task_canvas_tag_manager.UseCase

open FsUnit
open NUnit.Framework
open System
open task_canvas_tag_manager.Domain

module タグの削除のテスト =
    [<Test>]
    let タグの削除の成功 () =
        let mutable タグの削除の実行called = 0

        let deps: タグの削除.Deps = {
            タグの削除 = fun _ -> async { タグの削除の実行called <- タグの削除の実行called + 1 }
        }

        タグの削除.実行 deps (タグ番号 (Guid.NewGuid())) |> Async.RunSynchronously

        タグの削除の実行called |> should equal 1