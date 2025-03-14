#nullable enable
using UnityEngine;
using Safari.Model;
using System;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Safari.View.World.Map
{
    public class MapDisplay : MonoBehaviour
    {
        public class MapInitializedEventArgs : EventArgs
        {
            public Dictionary<Vector3, FieldDisplay> Displayers;

            public MapInitializedEventArgs(Dictionary<Vector3, FieldDisplay> displayers)
            {
                Displayers = displayers;
            }
        }

        public FieldDisplay FieldDisplayPrefab;

        public UnityEvent<MapInitializedEventArgs> MapInitialized;

        private FieldDisplay[,]? displayers;

        void Start()
        {
            SafariGame.Instance.Map.FieldChanged += Map_FieldChanged;
            BuildMap();
        }

        private async void Map_FieldChanged(object sender, GridPosition e)
        {
            if (displayers == null)
            {
                return;
            }
            // It waits until next Update, but the reason why it's needed is that it also
            // jumps back to the Unity thread
            await Awaiters.NextFrame;
            displayers[e.Z, e.X].DisplayField(SafariGame.Instance.Map.FieldAt(e));
        }

        void Update()
        {

        }

        private void OnDestroy()
        {
            // !!!Always unsubscribe from events after object in Unity is deleted
            SafariGame.Instance.Map.FieldChanged -= Map_FieldChanged;
            displayers = null;
        }

        private Dictionary<Vector3, FieldDisplay> BuildMap()
        {
            displayers = new FieldDisplay[SafariGame.Instance.Map.SizeZ, SafariGame.Instance.Map.SizeX];
            int sizeX = SafariGame.Instance.Map.SizeX;
            int sizeZ = SafariGame.Instance.Map.SizeZ;

            Dictionary<Vector3, FieldDisplay> displayerMap = new Dictionary<Vector3, FieldDisplay>();

            Vector3 center = new Vector3(
                (float)Math.Floor((sizeX - 1) / 2.0f),
                0,
                (float)Math.Floor((sizeZ - 1) / 2.0f));

            for (int x = 0; x < SafariGame.Instance.Map.SizeX; x++)
            {
                for (int z = 0; z < SafariGame.Instance.Map.SizeZ; z++)
                {
                    var display = Instantiate(
                        FieldDisplayPrefab,
                        (new Vector3(x, 0, z) - center) * 30,
                        Quaternion.identity,
                        new InstantiateParameters()
                        {
                            parent = transform,
                            worldSpace = false
                        });
                    GridPosition position = new GridPosition(x, z);
                    displayers[position.Z, position.X] = display;
                    display.Init(SafariGame.Instance.Map.FieldAt(position), position);
                }
            }
            return displayerMap;
        }
    }

}

