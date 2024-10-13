namespace task_canvas_tag_manager.UseCase

open System

open FsUnit
open NUnit.Framework
open task_canvas_tag_manager.Domain

module 全てのタグの取得のテスト =
    [<Test>]
    let 全てのタグの取得 () =
        let タグ一覧 =
            [ { タグ番号 = タグ番号 (Guid.NewGuid())
                名前 = タグ名 "タグ1" }
              { タグ番号 = タグ番号 (Guid.NewGuid())
                名前 = タグ名 "タグ2" } ]

        let mutable 全てのタグの取得called = 0

        let deps: 全てのタグの取得.Deps =
            { 全てのタグの取得 =
                fun () ->
                    async {
                        全てのタグの取得called <- 全てのタグの取得called + 1
                        return タグ一覧
                    } }

        let result = 全てのタグの取得.実行 deps |> Async.RunSynchronously
        result |> ignore

        全てのタグの取得called |> should equal 1
