namespace task_canvas_tag_manager.Handler

open Microsoft.AspNetCore.Http
open task_canvas_tag_manager.UseCase
open task_canvas_tag_manager.Domain


module CreateTag =
    type TagRequestJson = { name: string }

    let handler (deps: タグの登録.Deps) (name: string) : Async<IResult> =
        async {
            try
                let タグ名 = タグ名 name

                do! タグの登録.実行 deps タグ名

                return Results.Ok()
            with ex ->
                return Results.Problem(ex.Message)
        }
