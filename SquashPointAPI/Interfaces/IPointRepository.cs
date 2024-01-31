namespace SquashPointAPI.Interfaces;

public interface IPointRepository
{
    bool CreatePoint(int setId, string pointType);
    bool UpdateWinner(int playerId);
    bool Save();
}