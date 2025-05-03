using Safari.Model;
using Safari.Model.GameSpeed;
using Safari.Model.Jeep;
using Safari.Model.Map;
using Safari.Model.Rangers;
using Safari.View.Animals;
using Safari.View.Rangers;
using Safari.View.World.Map;
using System.Collections.Generic;
using UnityEngine;
using static Safari.View.World.Map.MapDisplay;

namespace Safari.View
{
    public class JeepCollectionController : MonoBehaviour
    {
		private const int JeepPrice = 15;
		private List<JeepDisplay> displayers;
		public JeepDisplay JeepDisplayPrefab;
        private JeepCollection jeepCollection;

		public List<JeepDisplay> Displayers { get { return displayers; } }

		private Dictionary<GridPosition, Vector3> gridPositionMapping;
		public Dictionary<GridPosition, Vector3> GridPositionMapping { get { return gridPositionMapping; } }

		
		public MapDisplay mapDisplayPrefab;
		
		private Map map;

		private MoneyManager moneyManager;

		void Start()
        {
			map = SafariGame.Instance.Map;
			moneyManager = SafariGame.Instance.MoneyManager;

			displayers = new List<JeepDisplay>();
			jeepCollection = SafariGame.Instance.Jeeps;
			jeepCollection.Added += OnJeepAdded;
			

		}

	
		public void OnJeepBuy()
		{
		
			Vector3 entranceWorldPos = Vector3.zero;
			foreach (var kvp in mapDisplayPrefab.DisplayerDict)
			{
				if (kvp.Value.Position.X == map.EntranceCoords.X &&
					kvp.Value.Position.Z == map.EntranceCoords.Z)
				{
					entranceWorldPos = kvp.Key;
					
					break;
				}
			}
			Debug.Log(entranceWorldPos);
            if (!moneyManager.CanBuy(JeepPrice))
            {
				return;
            }
			moneyManager.AddToBalance(-JeepPrice);
            jeepCollection.Add(new Jeep(entranceWorldPos));
			Debug.Log("Jeep Bought");
			
		}

        private void OnJeepAdded(object sender, Jeep jeep)
        {
			JeepDisplay display = Instantiate(JeepDisplayPrefab, jeep.Position,
				Quaternion.identity,
				new InstantiateParameters()
				{
					parent = transform,
					worldSpace = false
				});
			display.Init(jeep, gridPositionMapping);
			
			displayers.Add(display);
		}


		public void InjectGridPositionMappingData(MapDisplay.MapInitializedEventArgs args)
		{
			gridPositionMapping = new Dictionary<GridPosition, Vector3>();
			foreach (var item in args.Displayers)
			{
				gridPositionMapping.Add(item.Value.Position, item.Key);
			}
		}


		// Update is called once per frame
		void Update()
        {
        
        }
    }
}
