namespace task_canvas_tag_manager.UseCase

open FsUnit
open NUnit.Framework
open task_canvas_tag_manager.Domain
open System

module タグの更新のテスト =
    [<Test>]
    let タグの更新の成功 () =
        let mutable タグの更新called = 0

        let deps : タグの更新.Deps = {
            タグの更新 =
                fun (タグ: タグ) ->
                    async {
                        match タグ.名前 with
                        | タグ名 "タグ1" -> タグの更新called <- タグの更新called + 1
                        | _ -> ()
                    }
        }

        タグの更新.実行 deps { 名前 = タグ名 "タグ1"; タグ番号 = タグ番号 (Guid.NewGuid()) } |> Async.RunSynchronously

        タグの更新called |> should equal 1