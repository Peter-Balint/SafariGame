#nullable enable
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

        public static SafariGame Instance
        {
            get
            {
                if (instance == null)
                {
                    StartGame();
                    return instance!;
                    //throw new InvalidOperationException("Cannot access Safari.Instance. No game is running currently.");
                }
                return instance;
            }
        }

        public Map Map { get; }

        public SafariGame(Map map)
        {
            Map = map;
        }

        public static void StartGame()
        {
            instance = new SafariGame(MapGenerator.GenerateMap(20, 20));
            var t= new System.Timers.Timer(4000) { Enabled = true, AutoReset=false};
            t.Elapsed += (_, __) =>
            {
                for (global::System.Int32 i = 0; i < 5; i++)
                {
                    for (global::System.Int32 j = 0; j < 5; j++)
                    {
                        instance.Map.ChangeFieldAt(new GridPosition(i +10, j + 10), new Water());
                    }
                }
                ;
            };
        }
    }
}
