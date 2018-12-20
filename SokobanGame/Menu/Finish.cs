using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SokobanGame
{
    public class Finish
    {
        public static Dictionary<string, Texture2D> Textures { get; private set; }

        private SpriteFont font;

        private string congratulations;

        public Finish(ContentManager content, GraphicsDeviceManager graphics)
        {
            Textures = TextureContent.LoadListContent<Texture2D>(content, "Finish");

            font = content.Load<SpriteFont>("font");
        }

        public void Update(int scores, double time, string name = "Дорогой игрок")
        {
            congratulations = string.Format("{0}, поздравляем, Вы выиграли!\nФинальный счет: {1},\nВремя: {2}.", name, scores, time);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, congratulations, new Vector2(100, 100), Color.Black);
        }
    }
}