module task_canvas_tag_manager.Config

type TaskCanvasApi = {
  Endpoint: string
}

let defaultTaskCanvasApi = { Endpoint = "http://localhost:8080" }