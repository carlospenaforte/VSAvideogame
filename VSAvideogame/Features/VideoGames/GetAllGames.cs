using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VSAvideogame.Data;
using VSAvideogame.Entities;

namespace VSAvideogame.Features.VideoGames
{
    public static class GetAllGames
    {
        public record Query : IRequest<IEnumerable<Response>>;

        public record Response(int Id, string Title, string Genre, int RealeaseYear);

        public class Handler(VideoGameDbContext context) : IRequestHandler<Query, IEnumerable<Response>>
        {
            public async Task<IEnumerable<Response>> Handle(Query request, CancellationToken cancellationToken)
            {
                var videoGames = await context.VideoGames.ToListAsync(cancellationToken);
                return videoGames.Select(vg => new Response(vg.Id, vg.Title, vg.Genre, vg.RealeaseYear));
            }   
        }

    }

    [ApiController]
    [Route("api/games")]
    public class GetAllGamesController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllGames.Response>>> GetAllGames()
        {
            var response = await sender.Send(new GetAllGames.Query());
            return Ok(response);
        }
    }
}
