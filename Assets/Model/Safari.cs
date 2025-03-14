#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model
{
    public class Safari
    {
        private static Safari? instance;

        public static bool IsGameStarted => instance != null;

        public static Safari Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new InvalidOperationException("Cannot access Safari.Instance. No game is running currently.");
                }
                return instance;
            }
        }

        public Map Map { get; }

        public Safari(Map map)
        {
            Map = map;
        }

        public static void StartGame()
        {
            instance = new Safari(MapGenerator.GenerateMap(50, 50));
        }
    }
}
