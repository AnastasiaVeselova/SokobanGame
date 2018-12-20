using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SokobanGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SokoGame sokoGame;
        private FinishGame finishGame;

        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;

        private const float TIMER = 0.1f;
        private float timer = TIMER;

        public static GameState State { get; set; }

        private MainMenu mainMenu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = 500;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            State = GameState.Menu;

            mainMenu = new MainMenu(Content, graphics);
            sokoGame = new SokoGame(Content, graphics);
            finishGame = new FinishGame(Content, graphics);

            Mouse.WindowHandle = Window.Handle;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            // TODO: Add your update logic here
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            switch (State)
            {
                case GameState.Menu:
                    mainMenu.Update();
                    break;

                case GameState.Finish:
                    finishGame.Update(sokoGame.CurrentScores);
                    break;

                case GameState.Game:
                    {

                        if (timer < 0)
                        {
                            var pressedKeys = currentKeyboardState.GetPressedKeys();

                            if (pressedKeys.Length == 1)
                                sokoGame.Update(currentKeyboardState);


                            timer = TIMER;
                            base.Update(gameTime);
                        }
                        break;
                    }
            }

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here


            spriteBatch.Begin();

            switch (State)
            {
                case GameState.Menu:
                    {
                        mainMenu.Draw(spriteBatch);

                        State = mainMenu.SetGameState();
                        break;
                    }

                case GameState.Game:
                    {
                        sokoGame.Draw(spriteBatch);
                        if (sokoGame.FinishGame)
                            State = GameState.Finish;
                        break;
                    }

                case GameState.Finish:
                    {
                        finishGame.Draw(spriteBatch);
                        break;
                    }
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
