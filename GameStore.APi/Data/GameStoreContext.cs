﻿using System.Reflection;
using GameStore.APi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.APi.Data;

public class GameStoreContext:DbContext
{
    public GameStoreContext(DbContextOptions<GameStoreContext> options):base(options)
    {
        
    }

    public DbSet<Game> Games=> Set<Game>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }


}
