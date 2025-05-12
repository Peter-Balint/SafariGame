using UnityEngine;
using System.Collections.Generic;
using Safari.Model.Rangers;
using Safari.Model.Map;
using Safari.Model;
using Safari.View.World.Map;
using Safari.Model.GameSpeed;
using System;


namespace Safari.View.Rangers
{
    public class RangerCollectionController : MonoBehaviour
    {
        private List<RangerDisplay> displayers;

        private RangerCollection rangerCollection;

        public RangerDisplay RangerDisplayPrefab;

        private Dictionary<GridPosition, Vector3> gridPositionMapping;
        public Dictionary<GridPosition, Vector3> GridPositionMapping { get { return gridPositionMapping; } }

        private GameSpeedManager gameSpeedManager;
        private Map map;

        [SerializeField]
        private int rangerSalary;

        public event EventHandler<RangerDisplay> OnRangerClicked;

        void Start()
        {
            rangerCollection = SafariGame.Instance.Rangers;
            gameSpeedManager = SafariGame.Instance.GameSpeedManager;
            map = SafariGame.Instance.Map;

            displayers = new List<RangerDisplay>();

            rangerCollection.Added += OnRangerAdded;
            rangerCollection.Removed += OnRangerRemoved;

            gameSpeedManager.DayPassed += (sender,args) => {  };    //when moneymanager is made
                                                                    //change to month also, misread the task
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
            Ranger ranger = new Ranger();
            ranger.Position = gridPositionMapping[new GridPosition(map.EntranceCoords.X, map.EntranceCoords.Z + 1)];
            rangerCollection.Add(ranger);
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
            display.Init(ranger, gridPositionMapping, gameSpeedManager);
            display.OnCLick += HandleRangerClicked;
            displayers.Add(display);
        }
        private void OnRangerRemoved(object sender, Ranger ranger)
        {
            foreach (RangerDisplay display in displayers)
            {
                if (display.Ranger == ranger)
                {
                    display.OnCLick -= HandleRangerClicked;
                    displayers.Remove(display);
                    Destroy(display.gameObject);
                    return;
                }
            }
        }

        private void HandleRangerClicked(object sender, EventArgs e)
        {
            if(sender is RangerDisplay rangerDisplay)
            {
                OnRangerClicked?.Invoke(this, rangerDisplay);
            }
        }
    }
}
