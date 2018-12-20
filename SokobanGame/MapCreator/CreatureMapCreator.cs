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
    public class CreatureMapCreator
    {

        public  List<List<Creature>[,]> CreateMap(List<string> mapsStr, string separator = "\r\n")
        {

            var resultList = new List<List<Creature>[,]>();

            foreach (var mapStr in mapsStr)
            {

                var rows = mapStr.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

                var result = new List<Creature>[rows[0].Length, rows.Length];

                for (var x = 0; x < rows[0].Length; x++)
                    for (var y = 0; y < rows.Length; y++)
                    {

                        result[x, y] = new List<Creature>();
                        var creature = CreateCreatureBySymbol(rows[y][x], new IntegerCoordinates(x, y));
                        if (creature != null)
                            result[x, y].Add(creature);
                    }
                resultList.Add(result);
            }
            return resultList;
        }



        private Creature CreateCreatureBySymbol(char c, IntegerCoordinates coordinates)
        {
            switch (c)
            {
                case 'P':
                    return new Player(coordinates);

                case 'B':
                    return new Box(coordinates);

                case 'S':
                    return new StorageLocation(coordinates);

                case 'W':
                    return new Wall(coordinates);

                case ' ':
                    return null;
                default:
                    throw new Exception($"wrong character for ICreature {c}");
            }
        }
    }
}
