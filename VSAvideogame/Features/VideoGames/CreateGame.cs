using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using VSAvideogame.Data;
using VSAvideogame.Entities;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace VSAvideogame.Features.VideoGames
{
    public static class CreateGame
    {
        public record Command(string Title, string Genre, int RealeaseYear) : IRequest<Response>;
        public record Response(int Id, string Title, string Genre, int RealeaseYear);
        public class Handler(VideoGameDbContext context) : IRequestHandler<Command, Response>
        {
            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var videoGame = new VideoGame
                {
                    Title = request.Title,
                    Genre = request.Genre,
                    RealeaseYear = request.RealeaseYear
                };
                context.VideoGames.Add(videoGame);

                await context.SaveChangesAsync(cancellationToken);

                return new Response(videoGame.Id, videoGame.Title, videoGame.Genre, videoGame.RealeaseYear);
            }
        }
    }

    [ApiController]
    [Route("api/games")]
    public class CreateGameController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<CreateGame.Response>> CreateGame(CreateGame.Command command)
        {
            var response = await sender.Send(command);

            return Created($"/api/games/{response.Id}", response);
        }
    }
}
