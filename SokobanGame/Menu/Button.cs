using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SokobanGame
{
    public enum CreatureButtonState
    {
        None,
        Pressed,
        Hover,
        Released
    }

    public class Button
    {



        private Rectangle rectangle;
        public CreatureButtonState State { get; set; }

        private Texture2D texture;

        public Button(Rectangle rectangle, Texture2D texture)
        {
            this.rectangle = rectangle;
            this.texture = texture;
        }

        public void Update(MouseState mouseState)
        {
            if (rectangle.Contains(mouseState.X, mouseState.Y))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    State = CreatureButtonState.Pressed;
                else
                    State = (State == CreatureButtonState.Pressed) ? CreatureButtonState.Released : CreatureButtonState.Hover;
            }
            else
                State = CreatureButtonState.None;
        }

        public void Draw(SpriteBatch s)
        {
            if (State == CreatureButtonState.Pressed)
                s.Draw(texture, rectangle, Color.Green);

            else if (State == CreatureButtonState.Hover)
                s.Draw(texture, rectangle, Color.Lime);

            else
                s.Draw(texture, rectangle, Color.White);
        }
    }
}
