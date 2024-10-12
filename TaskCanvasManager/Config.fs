namespace task_canvas_tag_manager.Config

open FsConfig

type TaskCanvasDbConfig =
    { [<DefaultValue("localhost")>]
      Host: string
      [<DefaultValue("5432")>]
      Port: int
      [<DefaultValue("developer")>]
      Username: string
      [<DefaultValue("task_canvas")>]
      Database: string
      [<DefaultValue("root")>]
      Password: string}

type Config = { TaskCanvasDb: TaskCanvasDbConfig }
