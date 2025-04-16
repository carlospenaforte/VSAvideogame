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
                var games = await context.VideoGames.ToListAsync(cancellationToken);
                return games.Select(g => new Response(g.Id, g.Title, g.Genre, g.RealeaseYear));
            }
        }

    }

    [ApiController]

}
