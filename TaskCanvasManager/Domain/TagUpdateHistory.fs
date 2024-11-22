namespace task_canvas_tag_manager.Domain

open System

type タグの更新履歴番号 = タグの更新履歴番号 of Guid

type タグの更新履歴の作成日時 = タグの更新履歴の作成日時 of DateTimeOffset

type タグの更新履歴 =
    { 履歴番号: タグの更新履歴番号
      作成日時: タグの更新履歴の作成日時
      タグ名: タグ名
      タグ番号: タグ番号 }

module タグの更新履歴 =
    module Port =
        type タグの更新履歴の登録 = タグの更新履歴 -> Async<unit>

    let タグの更新履歴の作成 (タグ: タグ) : タグの更新履歴 =
        { 履歴番号 = タグの更新履歴番号 (Guid.NewGuid())
          作成日時 = タグの更新履歴の作成日時 (DateTimeOffset.Now.ToUniversalTime())
          タグ名 = タグ.名前
          タグ番号 = タグ.タグ番号 }

    let 登録 (タグの更新履歴の作成: Port.タグの更新履歴の登録) (タグの更新履歴': タグの更新履歴) : Async<unit> = タグの更新履歴の作成 タグの更新履歴'
