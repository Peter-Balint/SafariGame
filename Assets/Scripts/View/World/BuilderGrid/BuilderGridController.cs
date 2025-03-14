#nullable enable
using Safari.Model;
using System;
using UnityEngine;
using static Safari.View.World.Map.MapDisplay;

namespace Safari.View.World.BuilderGrid
{
    public class BuilderGridController : MonoBehaviour
    {
        public BuilderCell BuilderCellPrefab;

        private BuilderCell[,]? cells;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnMapInitialized(MapInitializedEventArgs args)
        {
            if (cells != null)
            {
                throw new InvalidOperationException("BuilderGridController received OnMapInitialized event more than once");
            }
            cells = new BuilderCell[SafariGame.Instance.Map.SizeZ, SafariGame.Instance.Map.SizeX];
            foreach (var pair in args.Displayers)
            {
                var worldPos = pair.Key;
                var fieldDislay = pair.Value;
                cells[fieldDislay.Position.Z, fieldDislay.Position.X] = Instantiate(BuilderCellPrefab,
                            worldPos,
                            Quaternion.identity,
                            new InstantiateParameters() { parent = transform, worldSpace = false });
            }
        }
    }
}
