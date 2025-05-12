#nullable enable
using Safari.Model.Animals;
using Safari.Model.Construction;
using Safari.Model.Map;
using Safari.Model.Pathfinding;
using Safari.Model.Rangers;
using Safari.Model.GameSpeed;
using Safari.Model.Hunters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Safari.Model.Jeeps;
using Safari.Model.Assets;

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

        public Map.Map Map { get; }

        public ConstructionManager Construction { get; }

        public MoneyManager MoneyManager { get; }

        public AnimalCreationManager AnimalCreationManager { get; }

        public AnimalCollection Animals { get; }

        public RangerCollection Rangers { get; }

        public JeepCollection Jeeps { get; }
        public HunterCollection Hunters { get; }
        public VisitorManager Visitors { get; }

        private PathfindingHelper pathfinding;

        public GameSpeedManager GameSpeedManager { get; }

        public SafariGame(Map.Map map, GameDifficulty gameDifficulty)
        {
            Map = map;
            Difficulty = gameDifficulty;
            pathfinding = new PathfindingHelper(map);
            Animals = new AnimalCollection(pathfinding);
            MoneyManager = new MoneyManager(Animals);
            AnimalCreationManager = new AnimalCreationManager(Animals, pathfinding, Map, MoneyManager);
            Construction = new ConstructionManager(map, MoneyManager);
            GameSpeedManager = new GameSpeedManager();
            Hunters = new HunterCollection();
            Rangers = new RangerCollection();
            Visitors = new VisitorManager(MoneyManager, GameSpeedManager);
            Jeeps = new JeepCollection(Visitors, pathfinding);
        }

        public static void StartGame(GameDifficulty gameDifficulty)
        {
            instance = new SafariGame(MapGenerator.GenerateMap(30, 30), gameDifficulty);
        }
    }
}
