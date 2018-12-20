using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SokobanGame
{
    class Wall : Creature
    {
        public Wall(IntegerCoordinates coordinates) : base(coordinates)
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
            return "wall";
        }
    }
}
