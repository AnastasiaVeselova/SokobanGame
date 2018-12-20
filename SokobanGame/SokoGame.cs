using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SokobanGame
{
    public class SokoGame
    {
        public static Dictionary<string, Texture2D> Textures { get; private set; }

        public static Dictionary<string, SpriteFont> Fonts { get; private set; }

        private List<Creature>[,] Map;

        private List<List<Creature>[,]> Maps;

        public int GetMapHeight() => Map.GetLength(1);
        public int GetMapWidth() => Map.GetLength(0);

        private CreatureMapCreator mapCreator;

        public int StorageLocationCount { get; set; }

        public int Scores { get; set; }
        public int CurrentScores { get; set; }

        public bool FinishGame { get; set; }

        public bool IsFinishedLevel() => StorageLocationCount == 0;

        private int widthLoc;
        private int heightLoc;

        private int textureSize;

        private int levelNumber;

        private GraphicsDeviceManager graphics;

        public SokoGame(ContentManager content, GraphicsDeviceManager graphics)
        {

            this.graphics = graphics;

            mapCreator = new CreatureMapCreator();

            Textures = LoadContent.LoadListContent<Texture2D>(content, "Graphics");

            Fonts = LoadContent.LoadListContent<SpriteFont>(content, "Font");

            var mapsStrList = LoadFiles.LoadFilesFromDirectory();

            Maps = mapCreator.CreateMap(mapsStrList);

            Scores = 0;
            levelNumber = 0;
            SetNextMap(levelNumber);
        }


        private bool SetNextMap(int k)
        {
            if (k >= Maps.Count)
                return false;

            Map = Maps[k];
            StorageLocationCount = 0;
            CurrentScores = 0;


            for (int i = 0; i < GetMapHeight(); i++)
                for (int j = 0; j < GetMapWidth(); j++)
                {
                    var coords = new IntegerCoordinates(j, i);
                    Map[j, i] = SortCreatureList(coords);
                    if (GetUpperCreature(coords) is StorageLocation)
                        StorageLocationCount++;

                }
            ComputeTextureSize();
            return true;
        }

        private void ComputeTextureSize()
        {
            textureSize = GetMapHeight() > GetMapWidth()
                ? graphics.PreferredBackBufferHeight / (GetMapHeight() + 2)
                : graphics.PreferredBackBufferWidth / (GetMapWidth() + 2);

            widthLoc = (graphics.PreferredBackBufferWidth - textureSize * (GetMapWidth() + 2)) / 2;
            heightLoc = (graphics.PreferredBackBufferHeight - textureSize * (GetMapHeight() + 2)) / 2;

        }



        public Creature GetUpperCreature(IntegerCoordinates coordinates)
        {
            return Map[coordinates.X, coordinates.Y].Count != 0
                ? Map[coordinates.X, coordinates.Y][0]
                : null;
        }

        public Creature GetLowerCreature(IntegerCoordinates coordinates)
        {
            var creatureList = Map[coordinates.X, coordinates.Y];
            return creatureList.Count != 0
                ? creatureList[creatureList.Count - 1]
                : null;
        }

        private List<Creature> SortCreatureList(IntegerCoordinates coordinates)
        {
            return Map[coordinates.X, coordinates.Y].OrderBy(c => c.GetDrawingPriority()).ToList();
        }

        private void AddCreature(Creature creature, IntegerCoordinates coordinates)
        {
            Map[coordinates.X, coordinates.Y].Add(creature);
            Map[coordinates.X, coordinates.Y] = SortCreatureList(coordinates);

            /*if (GetLowerCreature(coordinates) is StorageLocation)
            {
                StorageLocationCount--;

                var upperCreature = GetUpperCreature(coordinates);
                if (upperCreature is Box)
                    upperCreature.OnStorageLocation = true;
            }*/


        }

        private void RemoveCreature(Creature creature, IntegerCoordinates coordinates)
        {
            Map[coordinates.X, coordinates.Y].Remove(creature);

            /* if (GetUpperCreature(coordinates) is StorageLocation)
             {
                 StorageLocationCount++;
                 if (creature is Box)
                     creature.OnStorageLocation = false;
             }*/
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures["gameBackground"], new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);

            for (int i = 0; i < GetMapHeight(); i++)
            {
                for (int j = 0; j < GetMapWidth(); j++)
                {
                    var creature = GetUpperCreature(new IntegerCoordinates(j, i));

                    if (creature != null)
                    {
                        creature.Draw(spriteBatch, new Rectangle(widthLoc + (j + 1) * textureSize, heightLoc + (i + 1) * textureSize, textureSize, textureSize));
                        creature.IsActive = true;
                    }
                }
            }
            spriteBatch.DrawString(Fonts["font"], string.Format("Current scores: {0}  Scores: {1}  \nFree storage location count: {2} ", CurrentScores, Scores, StorageLocationCount), new Vector2(50, 440), Color.Black);



            FinishGame = IsFinishedLevel() && !SetNextMap(++levelNumber);

        }

        public void Update(KeyboardState currentKeyboardState)
        {
            if (currentKeyboardState.GetPressedKeys().Length == 0)
                return;


            var pressedKey = currentKeyboardState.GetPressedKeys().First().ToString();
            if (!(pressedKey == "Right" || pressedKey == "Left" || pressedKey == "Up" || pressedKey == "Down"))
                return;


            for (int i = 0; i < GetMapHeight(); i++)
            {
                for (int j = 0; j < GetMapWidth(); j++)
                {
                    var creature = GetUpperCreature(new IntegerCoordinates(j, i));

                    if (creature != null)
                    {
                        var creatureMoving = creature.Act(this, currentKeyboardState);

                        foreach (var creatureMovingItem in creatureMoving)
                        {
                            AddCreature(creatureMovingItem.Creature, creatureMovingItem.TargetCoords);
                            RemoveCreature(creatureMovingItem.Creature, creatureMovingItem.Coords);
                            creatureMovingItem.Creature.Coordinates = creatureMovingItem.TargetCoords;

                            Scores += creatureMovingItem.ScoresDelta;
                            CurrentScores += creatureMovingItem.ScoresDelta;


                            if (creatureMovingItem.FreeStorageLocationDelta == 1)
                                creatureMovingItem.Creature.OnStorageLocation = false;
                            if (creatureMovingItem.FreeStorageLocationDelta == -1)
                                creatureMovingItem.Creature.OnStorageLocation = true;

                            /*if (creatureMovingItem.Creature is Player)
                                Scores++;*/

                            StorageLocationCount += creatureMovingItem.FreeStorageLocationDelta;
                        }
                    }
                }
            }
        }
    }
}