#nullable enable
using Safari.Model;
using System;
using UnityEngine;
using UnityEngine.Events;
using static Safari.View.World.Map.MapDisplay;

namespace Safari.View.World.BuilderGrid
{
    public class BuilderGridController : MonoBehaviour
    {
        public BuilderCell BuilderCellPrefab;

        public UnityEvent<GridPosition> Click;

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
                BuilderCell cell = Instantiate(BuilderCellPrefab,
                                               worldPos,
                                               Quaternion.identity,
                                               new InstantiateParameters()
                                               {
                                                   parent = transform,
                                                   worldSpace = true,

                                               });
                cells[fieldDislay.Position.Z, fieldDislay.Position.X] = cell;
                cell.Click.AddListener(() => OnCellClick(fieldDislay.Position));
            }
        }

        public void Open()
        {
            enabled = true;
        }

        public void Close()
        {
            enabled = false;
        }

        private void OnCellClick(GridPosition position)
        {
            Click?.Invoke(position);
        }
    }
}
