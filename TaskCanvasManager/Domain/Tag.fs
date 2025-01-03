namespace task_canvas_tag_manager.Domain

open System

type タグ番号 = タグ番号 of Guid
type タグ名 = タグ名 of string

type タグ = { タグ番号: タグ番号; 名前: タグ名 }

module タグ =
    module Port =
        type 全てのタグの取得 = unit -> Async<タグ list>
        type タグの登録 = タグ -> Async<unit>
        type タグの更新 = タグ -> Async<unit>
        type タグの削除 = タグ番号 -> Async<unit>
        type タグの検索 = タグ名 -> Async<タグ list>

    let タグの作成 (名前: タグ名) : タグ =
        { タグ番号 = タグ番号 (Guid.NewGuid())
          名前 = 名前 }

    let 登録 (タグの登録: Port.タグの登録) (タグ': タグ) : Async<unit> = タグの登録 タグ'

    let 更新 (タグの更新: Port.タグの更新) (タグ': タグ) : Async<unit> = タグの更新 タグ'

    let 削除 (タグの削除: Port.タグの削除) (タグ番号': タグ番号) : Async<unit> = タグの削除 タグ番号'

    let 検索 (タグの検索: Port.タグの検索) (名前: タグ名) : Async<タグ list> = タグの検索 名前

    let 全件取得 (全てのタグの取得: Port.全てのタグの取得) : Async<タグ list> = 全てのタグの取得 ()
