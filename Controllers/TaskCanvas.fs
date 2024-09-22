module Controllers.TaskCanvas

open Microsoft.AspNetCore.Http

module GetTags =
    type Tag = { id: string; name: string }
    type ResponseJson = { tags: Tag list }

    let controller () : IResult =
        let tags = [
            { id = "3B893F5A-E9DC-4CF9-AC23-599AD2BD7FD4"; name = "Tag1" }
            { id = "907B39A8-4DAA-4E8B-930E-D6F87A935003"; name = "Tag2" }
        ]

        Results.Ok { tags = tags }