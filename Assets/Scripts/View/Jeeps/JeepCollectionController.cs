using Safari.Model;
using Safari.Model.GameSpeed;
using Safari.Model.Jeeps;
using Safari.Model.Map;
using Safari.Model.Rangers;
using Safari.View.Animals;
using Safari.View.Rangers;
using Safari.View.World.Map;
using System;
using System.Collections.Generic;
using UnityEngine;
using static PlasticPipe.PlasticProtocol.Messages.Serialization.ItemHandlerMessagesSerialization;
using static Safari.View.World.Map.MapDisplay;

namespace Safari.View.Jeeps
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

		/// <summary>
		///This method is hooked to the Jeep button in the UI.It checks where the entrance is, spawns the Jeep there, and subtracts the price of the Jeep if possible.        
		/// </summary>
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
            jeepCollection.CreateNewJeep(entranceWorldPos);
			Debug.Log("Jeep Bought");
			
		}
		/// <summary>
		/// Instantiates a Jeep display at the Jeep's position when a new Jeep is added,
		/// initializes it with the provided grid position mapping, and adds it to the list of displayers.
		/// </summary>
		/// <param name="sender">The event sender.</param>
		/// <param name="jeep">The Jeep that was added.</param>
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


	
    }
}
