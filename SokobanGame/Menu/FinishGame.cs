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
    public class FinishGame
    {
        public static Dictionary<string, Texture2D> Textures { get; private set; }

        public static Dictionary<string, SpriteFont> Fonts { get; private set; }

        private string congratulations;

        private GraphicsDeviceManager graphics;

        public FinishGame(ContentManager content, GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;

            Textures = LoadContent.LoadListContent<Texture2D>(content, "Finish");

            Fonts = LoadContent.LoadListContent<SpriteFont>(content, "Font");
        }

        public void Update(int scores, string name = "Player")
        {
            congratulations = string.Format("{0},congratulations!\nYou won!\nFinal game score: {1}.", name, scores);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures["finishBackground"], new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            spriteBatch.DrawString(Fonts["finish"], congratulations, new Vector2(30, 130), Color.Blue);
        }
    }
}