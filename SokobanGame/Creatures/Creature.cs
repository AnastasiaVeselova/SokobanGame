using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SokobanGame
{
    public abstract class Creature
    {
        public bool IsActive { get; set; }

        public IntegerCoordinates Coordinates { get; set; }

        public KeyboardState KeyboardState { get; set; } 

        public bool OnStorageLocation { get; set; }
        

        public Creature(IntegerCoordinates coordinates)
        {
            IsActive = true;
            Coordinates = coordinates;
            OnStorageLocation = false;
        }

        public abstract string GetTextureName();

        public abstract float GetDrawingPriority();

       
        public abstract List<CreatureMapChange> Act(SokoGame gameState, KeyboardState command);


        public void Draw(SpriteBatch spriteBatch, Rectangle PictureRectangle)
        {
            spriteBatch.Draw(SokoGame.Textures[GetTextureName()], PictureRectangle, Color.White);
        }
    }
}
