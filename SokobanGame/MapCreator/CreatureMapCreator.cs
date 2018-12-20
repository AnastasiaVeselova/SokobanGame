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
    public /*static*/ class CreatureMapCreator
    {
        //private /*static readonly*/ Dictionary<string, Texture2D> textures;



        public /*static*/ List<List<Creature>[,]> CreateMap(List<string> mapsStr, string separator = "\r\n")
        {
            //this.textures = textures;
            //подумоть, как в дальнейшем расширить на несколько лабиринтов

            //как вариант передавать передавать папку или список строк - названий 

            // и с ними создать коллекцию, которую и возвращать
            var resultList = new List<List<Creature>[,]>();

            foreach (var mapStr in mapsStr)
            {

                var rows = mapStr.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

                //if (rows.Select(z => z.Length).Distinct().Count() != 1)
                //throw new Exception($"Wrong test map '{map}'");

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

        /*private static ICreature CreateCreatureByTypeName(string name)
        {
            // Это использование механизма рефлексии. 
            // Ему посвящена одна из последних лекций второй части курса Основы программирования
            // В обычном коде можно было обойтись без нее, но нам нужно было написать такой код,
            // который работал бы, даже если вы ещё не создали класс Monster или Gold. 
            // Просто написать new Gold() мы не могли, потому что это не скомпилировалось бы пока вы не создадите класс Gold.
            if (!factory.ContainsKey(name))
            {
                var type = Assembly
                    .GetExecutingAssembly()
                    .GetTypes()
                    .FirstOrDefault(z => z.Name == name);
                if (type == null)
                    throw new Exception($"Can't find type '{name}'");
                factory[name] = () => (ICreature)Activator.CreateInstance(type);
            }

            return factory[name]();
        }*/


        private /*static*/ Creature CreateCreatureBySymbol(char c, IntegerCoordinates coordinates)
        {
            switch (c)
            {
                case 'P':
                    return new Player(/*textures[nameof(Player)],*/ coordinates);
                //создать игрока

                case 'B':
                    return new Box(/*textures[nameof(Box)],*/ coordinates);
                //создать коробку

                case 'S':
                    return new StorageLocation(/*textures[nameof(StorageLocation)],*/ coordinates);
                //создать цель

                case 'W':
                    return new Wall(/*textures[nameof(Wall)],*/ coordinates);
                //создать стену

                case ' ':
                    return null;
                default:
                    throw new Exception($"wrong character for ICreature {c}");
            }
        }
    }
}
