namespace task_canvas_tag_manager.Domain

open System

type タグ番号 = タグ番号 of Guid
type タグ名 = タグ名 of string

type タグ = { タグ番号: タグ番号; 名前: タグ名 }

module タグ =
    let タグの作成 (名前: タグ名) : タグ =
        { タグ番号 = タグ番号 (Guid.NewGuid())
          名前 = 名前 }
