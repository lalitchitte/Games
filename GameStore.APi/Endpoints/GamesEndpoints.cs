using GameStore.APi.Dtos;
using GameStore.Api.Entities;
using GameStore.APi.Entities;
using GameStore.APi.Repositories;

namespace GameStore.APi.Endpoints;

public static class GamesEndpoints
{
    const string GetGame = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {
        //for group to multiple endpoints.
        var group = routes.MapGroup("/games").WithParameterValidation();

        //Get /games
        group.MapGet(
            "/",
            async (IGamesRepository repository) =>
                (await repository.GetAllAsync()).Select(game => game.AsDto())
        // if (games == null || !games.Any())
        // {
        //     var errorResponse = new ApiResponse<IEnumerable<Game>>(false, "No games found",[]);
        //     return Results.NotFound(errorResponse);
        // }

        // var response = new ApiResponse<IEnumerable<Game>>(true, "Games found successfully", games);
        // return Results.Ok(response);
        );

        //Get /games/1
        group
            .MapGet(
                "/{id}",
                async (IGamesRepository repository, int id) =>
                {
                    Game? game = await repository.GetAsync(id);
                    return game is not null
                        ? Results.Ok(game.AsDto())
                        : Results.NotFound("Game not found");
                }
            )
            .WithName(GetGame);

        //Post /games
        group.MapPost(
            "/",
            async (IGamesRepository repository, CreateGameDtos gameDtos) =>
            {
                Game game =
                    new()
                    {
                        Name = gameDtos.Name,
                        Genre = gameDtos.Genre,
                        Price = gameDtos.Price,
                        ReleaseDate = gameDtos.ReleaseDate,
                        ImageUri = gameDtos.ImageUri
                    };
                await repository.CreateAsync(game);
                return Results.CreatedAtRoute(GetGame, new { id = game.Id }, game);
            }
        );

        //Put /games/1
        group.MapPut(
            "/{id}",
            async (IGamesRepository repository, int id, UpdateGameDtos updateGameDto) =>
            {
                Game? existingGame = await repository.GetAsync(id);

                if (existingGame is null)
                {
                    return Results.NotFound();
                }

                existingGame.Name = updateGameDto.Name;
                existingGame.Genre = updateGameDto.Genre;
                existingGame.Price = updateGameDto.Price;
                existingGame.ReleaseDate = updateGameDto.ReleaseDate;
                existingGame.ImageUri = updateGameDto.ImageUri;

                await repository.UpdateAsync(existingGame);

                return Results.NoContent();
            }
        );

        //Delete /games/1
        group.MapDelete(
            "/{id}",
            async (IGamesRepository repository, int id) =>
            {
                Game? game = await repository.GetAsync(id);

                if (game is not null)
                {
                    await repository.DeleteAsync(id);
                }
                return Results.NoContent();
            }
        );

        return group;
    }
}
