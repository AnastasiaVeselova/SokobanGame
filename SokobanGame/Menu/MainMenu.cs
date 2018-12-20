using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SokobanGame
{
    public class MainMenu
    {
        public static Dictionary<string, Texture2D> Textures { get; private set; }

        public Dictionary<string, Button> Buttons { get; private set; }

        private GraphicsDeviceManager graphics;

        public MainMenu(ContentManager content, GraphicsDeviceManager graphics)
        {
            Textures = TextureContent.LoadListContent<Texture2D>(content, "MainMenu");

            this.graphics = graphics;

            Buttons = new Dictionary<string, Button>()
            {
                //когда-нибудь убрать магические чиселки
                {"start", new Button(new Rectangle(150,90,200,60), Textures["startGame"]) },
                {"rules", new Button(new Rectangle(150,170,200,60), Textures["rules"]) },
                {"about", new Button(new Rectangle(150,250,200,60), Textures["about"]) },
                {"exit", new Button(new Rectangle(150,330,200,60), Textures["exit"]) }
            };


        }

        public void Update()
        {
            var mouseState = Mouse.GetState();
            foreach (var button in Buttons)
                button.Value.Update(mouseState);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures["mainBackground"], new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);


            foreach (var button in Buttons)
                button.Value.Draw(spriteBatch);
        }

        public GameState SetGameState()
        {
            if (Buttons["start"].State == CreatureButtonState.Released)
                return GameState.Game;

            if (Buttons["rules"].State == CreatureButtonState.Released)
                return GameState.Rules;

            if (Buttons["about"].State == CreatureButtonState.Released)
                return GameState.About;

            if (Buttons["exit"].State == CreatureButtonState.Released)
                return GameState.Exit;

            return GameState.Menu;
        }


    }
}
