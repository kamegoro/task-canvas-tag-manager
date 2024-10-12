namespace task_canvas_tag_manager

open System

type WeatherForecast =
    { Date: DateTime
      TemperatureC: int
      Summary: string }

    member this.TemperatureF = 32.0 + (float this.TemperatureC / 0.5556)
