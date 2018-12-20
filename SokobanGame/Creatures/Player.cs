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
    class Player : Creature
    {
        public Player(IntegerCoordinates coordinates) : base(coordinates)
        {

        }

        public override List<CreatureMapChange> Act(SokoGame gameState, KeyboardState keyboardState)
        {
            var creatureMovingList = new List<CreatureMapChange>();

            if (IsActive)
            {
                KeyboardState = keyboardState;

                var nextCoords = GetNextCoords(gameState, Coordinates);

                if (gameState.GetUpperCreature(nextCoords) == null
                    || gameState.GetUpperCreature(nextCoords) is StorageLocation)
                {
                    creatureMovingList.Add(new CreatureMapChange
                    {
                        Creature = this,
                        Coords = Coordinates,
                        TargetCoords = nextCoords,

                        ScoresDelta = 1,
                        FreeStorageLocationDelta = 0
                    });
                }
                else if (gameState.GetUpperCreature(nextCoords) is Box)
                {
                    var nextnextCoords = GetNextCoords(gameState, nextCoords);

                    if (gameState.GetUpperCreature(nextnextCoords) == null
                        || gameState.GetUpperCreature(nextnextCoords) is StorageLocation)
                    {
                        creatureMovingList.Add(new CreatureMapChange
                        {
                            Creature = this,
                            Coords = Coordinates,
                            TargetCoords = nextCoords,

                            ScoresDelta = 1,
                            FreeStorageLocationDelta = 0
                        });

                        int freeStorageLocationDelta = 0;
                        if (gameState.GetLowerCreature(nextCoords) is StorageLocation) freeStorageLocationDelta++;
                        if (gameState.GetUpperCreature(nextnextCoords) is StorageLocation) freeStorageLocationDelta--;

                        creatureMovingList.Add(new CreatureMapChange
                        {
                            Creature = gameState.GetUpperCreature(nextCoords),
                            Coords = nextCoords,
                            TargetCoords = nextnextCoords,

                            ScoresDelta = 0,
                            FreeStorageLocationDelta = freeStorageLocationDelta
                        });
                    }
                }
                IsActive = false;
            }

            return creatureMovingList;
        }


        public override float GetDrawingPriority()
        {
            return 0;
        }

        public override string GetTextureName()
        {
            if (KeyboardState.IsKeyDown(Keys.Down))
                return "characterDown";

            else if (KeyboardState.IsKeyDown(Keys.Right))
                return "characterRight";

            else if (KeyboardState.IsKeyDown(Keys.Left))
                return "characterLeft";

            else return "characterUp";
        }

        private IntegerCoordinates GetNextCoords(SokoGame game, IntegerCoordinates coords)
        {
            int dX = 0;
            int dY = 0;

            if (KeyboardState.IsKeyDown(Keys.Down) && coords.Y + 1 < game.GetMapHeight())
                dY = 1;

            if (KeyboardState.IsKeyDown(Keys.Up) && coords.Y > 0)
                dY = -1;

            if (KeyboardState.IsKeyDown(Keys.Right) && coords.X + 1 < game.GetMapWidth())
                dX = 1;

            if (KeyboardState.IsKeyDown(Keys.Left) && coords.X > 0)
                dX = -1;

            return new IntegerCoordinates(coords.X + dX, coords.Y + dY);
        }
    }
}
