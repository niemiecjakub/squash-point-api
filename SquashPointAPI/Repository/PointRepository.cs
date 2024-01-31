using SquashPointAPI.Data;
using SquashPointAPI.Interfaces;

namespace SquashPointAPI.Repository;

public class PointRepository(DataContext context) : IPointRepository
{
    public bool CreatePoint(int setId, string pointType)
    {
        throw new NotImplementedException();
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