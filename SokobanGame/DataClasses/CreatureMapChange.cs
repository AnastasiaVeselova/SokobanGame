using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SokobanGame
{
    public class CreatureMapChange
    {
        public Creature Creature;

        public IntegerCoordinates Coords;
        public IntegerCoordinates TargetCoords;

        public int ScoresDelta;

        public int FreeStorageLocationDelta;

    }
}
