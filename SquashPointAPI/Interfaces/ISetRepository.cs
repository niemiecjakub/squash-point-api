namespace SquashPointAPI.Interfaces;

public interface ISetRepository
{
    bool CreateSet(int gameId);
    bool UpdateWinner(int playerId);
    bool Save();
}