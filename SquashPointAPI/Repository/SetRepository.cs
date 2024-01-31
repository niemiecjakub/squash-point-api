using SquashPointAPI.Data;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointAPI.Repository;

public class SetRepository(DataContext context) : ISetRepository
{
    public bool CreateSet(int gameId)
    {
        var game = context.Games.Where(g => g.Id == gameId).FirstOrDefault();
        var set = new Set()
        {
            Game = game,
            Winner = null
        };
        
        context.Set.Add(set);

        return Save();
    }

    public bool UpdateWinner(int playerId)
    {
        throw new NotImplementedException();
    }
    public bool Save()
    {
        var saved = context.SaveChanges();
        return saved > 0 ? true : false;
    }
}