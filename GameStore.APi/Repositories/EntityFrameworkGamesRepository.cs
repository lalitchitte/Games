﻿using GameStore.APi.Data;
using GameStore.APi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.APi.Repositories;

public class EntityFrameworkGamesRepository : IGamesRepository
{

    private readonly GameStoreContext dbContext;

    public EntityFrameworkGamesRepository(GameStoreContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await dbContext.Games.AsNoTracking().ToListAsync();
    }

    public async Task<Game?> GetAsync(int id)
    {
        return await dbContext.Games.FindAsync(id);
    }

    public async Task CreateAsync(Game game)
    {
       dbContext.Games.Add(game);
       await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Game updateGame)
    {
        dbContext.Update(updateGame);
       await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
       await dbContext.Games.Where(game=>game.Id==id).ExecuteDeleteAsync();
    }
}
