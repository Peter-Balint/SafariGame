#nullable enable
using Safari.Model.Assets.Model.Animals;
using Safari.Model.Construction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Safari.Model
{
    public class SafariGame
    {
        private static SafariGame? instance;

        public static bool IsGameStarted => instance != null;

        public static GameDifficulty Difficulty;

        public static SafariGame Instance
        {
            get
            {
                if (instance == null)
                {
                    StartGame(GameDifficulty.Easy);
                    return instance!;
                    // throw new InvalidOperationException("Cannot access Safari.Instance. No game is running currently.");
                }
                return instance;
            }
        }

        public Map Map { get; }

        public ConstructionManager Construction { get;  }

        public AnimalCollection Animals { get;  }

        public SafariGame(Map map, GameDifficulty gameDifficulty)
        {
            Map = map;
            Difficulty = gameDifficulty;
            Construction = new ConstructionManager(map);
            Animals = new AnimalCollection();

        }

        public static void StartGame(GameDifficulty gameDifficulty)
        {
            instance = new SafariGame(MapGenerator.GenerateMap(20, 20), gameDifficulty);
        }
    }
}
