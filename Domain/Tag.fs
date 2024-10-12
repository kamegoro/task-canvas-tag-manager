module task_canvas_tag_manager.Domain

type タグ番号 = タグ番号 of string
type タグ名 = タグ名 of string

type タグ = { タグ番号: タグ番号; 名前: タグ名 }
