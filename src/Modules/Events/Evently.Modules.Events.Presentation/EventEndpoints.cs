﻿using Evently.Modules.Events.Presentation.Events;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation;

public static class EventEndpoints
{
    public static void MapEventsEndpoints(IEndpointRouteBuilder app)
    {
        CreateEvent.MapEndpoint(app);
        GetEvent.MapEndpoint(app);
    }
}
