using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SokobanGame
{
    class StorageLocation : Creature
    {
        public StorageLocation(IntegerCoordinates coordinates) : base(coordinates)
        {
        }

        public override List<CreatureMapChange> Act(SokoGame gameState, KeyboardState command)
        {
            return new List<CreatureMapChange>();
        }

        public override float GetDrawingPriority()
        {
            return 1;
        }

        public override string GetTextureName()
        {
            return "storageLocation";
        }

    }
}
