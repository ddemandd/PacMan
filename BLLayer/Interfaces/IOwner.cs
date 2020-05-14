using BLLayer.Enums;

namespace BLLayer.Interfaces
{
    public interface IOwner
    {
        public bool CanMoving(Coord currentCoord, Direction someDirection);

        public Coord GetNextCoord(Coord currentPosition, Direction someDirection);

        public void TryEatFood(Coord point);

        public bool CheckEnemys(Coord point);

        public bool CheckPacman(Coord point);

        public void PutPacmanToStartPosition();

        public void PutEnemyToStartPosition(Coord enemyPosition = null);

        public bool IsPacmanAngry();
    }
}
