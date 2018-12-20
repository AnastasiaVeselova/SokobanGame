using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SokobanGame
{
    public class Box : Creature
    {

        public Box(IntegerCoordinates coordinates) : base(coordinates)
        {

        } 

        public override float GetDrawingPriority()
        {
            return 0;
        }


        public override List<CreatureMapChange> Act(SokoGame gameState, KeyboardState command)
        {
            return new List<CreatureMapChange>();
        }

        public override string GetTextureName()
        {
            if (OnStorageLocation)
                return "boxOnStorageLocation";

            return "box";
        }
    }
}
