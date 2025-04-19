using UnityEngine;
using System.Collections.Generic;
using Safari.Model.Rangers;
using Safari.Model.Map;
using Safari.Model;
using Safari.View.World.Map;


namespace Safari.View.Rangers
{
    public class RangerCollectionController : MonoBehaviour
    {
        private List<RangerDisplay> displayers;

        private RangerCollection rangerCollection;

        public RangerDisplay RangerDisplayPrefab;

        private Dictionary<GridPosition, Vector3> gridPositionMapping;
        public Dictionary<GridPosition, Vector3> GridPositionMapping { get { return gridPositionMapping; } }


        void Start()
        {
            rangerCollection = SafariGame.Instance.Rangers;
            displayers = new List<RangerDisplay>();

            rangerCollection.Added += OnRangerAdded;
            rangerCollection.Removed += OnRangerRemoved;
        }

        public void InjectGridPositionMappingData(MapDisplay.MapInitializedEventArgs args)
        {
            gridPositionMapping = new Dictionary<GridPosition, Vector3>();
            foreach (var item in args.Displayers)
            {
                gridPositionMapping.Add(item.Value.Position, item.Key);
            }
        }

        public void OnRangerBuy() 
        {
            rangerCollection.Add(new Ranger());
        }

        private void OnRangerAdded(object sender, Ranger ranger)
        {
            RangerDisplay display = Instantiate(RangerDisplayPrefab, ranger.Position,
                Quaternion.identity,
                new InstantiateParameters()
                {
                    parent = transform,
                    worldSpace = false
                });
            display.Init(ranger, gridPositionMapping);
            displayers.Add(display);
        }
        private void OnRangerRemoved(object sender, Ranger ranger)
        {
            foreach (RangerDisplay display in displayers)
            {
                if (display.Ranger == ranger)
                {
                    displayers.Remove(display);
                    Destroy(display.gameObject);
                    return;
                }
            }
        }
    }
}
