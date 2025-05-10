using Safari.Model.GameSpeed;
using Safari.Model.Map;
using Safari.Model;
using Safari.Model.Hunters;
using Safari.View.World.Map;
using System.Collections.Generic;
using UnityEngine;

namespace Safari.View.Hunters
{
    public class HunterCollectionController : MonoBehaviour
    {
        private List<HunterDisplay> displayers;

        private HunterCollection hunterCollection;

        public HunterDisplay HunterDisplayPrefab;

        private Dictionary<GridPosition, Vector3> gridPositionMapping;
        public Dictionary<GridPosition, Vector3> GridPositionMapping { get { return gridPositionMapping; } }

        private GameSpeedManager gameSpeedManager;

        void Start()
        {
            hunterCollection = SafariGame.Instance.Hunters;
            gameSpeedManager = SafariGame.Instance.GameSpeedManager;

            displayers = new List<HunterDisplay>();

            hunterCollection.Added += OnHunterAdded;
            hunterCollection.Removed += OnHunterRemoved;
        }

        public void InjectGridPositionMappingData(MapDisplay.MapInitializedEventArgs args)
        {
            gridPositionMapping = new Dictionary<GridPosition, Vector3>();
            foreach (var item in args.Displayers)
            {
                gridPositionMapping.Add(item.Value.Position, item.Key);
            }
        }


        private void OnHunterAdded(object sender, Hunter hunter)
        {
            HunterDisplay display = Instantiate(HunterDisplayPrefab, hunter.Position,
                Quaternion.identity,
                new InstantiateParameters()
                {
                    parent = transform,
                    worldSpace = false
                });
            display.Init(hunter, gridPositionMapping, gameSpeedManager);
            displayers.Add(display);
        }
        private void OnHunterRemoved(object sender, Hunter hunter)
        {
            foreach (HunterDisplay display in displayers)
            {
                if (display.Hunter == hunter)
                {
                    displayers.Remove(display);
                    Destroy(display.gameObject);
                    return;
                }
            }
        }

        
    }
}
