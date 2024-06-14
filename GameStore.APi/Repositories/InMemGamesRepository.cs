using GameStore.APi.Entities;

namespace GameStore.APi.Repositories;

public class InMemGamesRepository : IGamesRepository
{
    private readonly List<Game> games =
        new()
        {
            new Game()
            {
                Id = 1,
                Name = "Street Fighter II",
                Genre = "Fighting",
                Price = 45.23M,
                ReleaseDate = new DateOnly(2013, 4, 2),
                ImageUri = "https://placehold.co/100"
            },
            new Game()
            {
                Id = 2,
                Name = "Final Fantasy I",
                Genre = "Role-Playing",
                Price = 44.33M,
                ReleaseDate = new DateOnly(2010, 6, 4),
                ImageUri = "https://placehold.co/100"
            },
            new Game()
            {
                Id = 3,
                Name = "FIFA 23",
                Genre = "Sports",
                Price = 57.95M,
                ReleaseDate = new DateOnly(2021, 8, 23),
                ImageUri = "https://placehold.co/100"
            }
        };

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await Task.FromResult(games);
    }

    public async Task<Game?> GetAsync(int id)
    {
        return await Task.FromResult(games.Find(game => game.Id == id));
    }

    public async Task CreateAsync(Game game)
    {
        game.Id = games.Max(game => game.Id) + 1;
        games.Add(game);

        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Game updateGame)
    {
        var index = games.FindIndex(game => game.Id == updateGame.Id);
        games[index] = updateGame;

        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var index = games.FindIndex(game => game.Id == id);
        games.RemoveAt(index);

        await Task.CompletedTask;
    }
}
