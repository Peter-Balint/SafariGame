#nullable enable
using Safari.Model;
using Safari.Model.Map;
using System;
using UnityEngine;
using UnityEngine.Events;
using static Safari.View.World.Map.MapDisplay;

namespace Safari.View.World.ConstructionGrid
{
    public class ConstructionGridController : MonoBehaviour
    {
        public GameObject ConstructionCellPrefab;

        public UnityEvent<GridPosition> Click;

        private ConstructionCell[,]? cells;

        public void OnMapInitialized(MapInitializedEventArgs args)
        {
            if (cells != null)
            {
                throw new InvalidOperationException($"{nameof(ConstructionGridController)} received {nameof(OnMapInitialized)} event more than once");
            }

            cells = new ConstructionCell[SafariGame.Instance.Map.SizeZ, SafariGame.Instance.Map.SizeX];
            foreach (var pair in args.Displayers)
            {
                var worldPos = pair.Key;
                var fieldDislay = pair.Value;
                GameObject obj = Instantiate(ConstructionCellPrefab,
                                             worldPos,
                                             Quaternion.identity,
                                             new InstantiateParameters()
                                             {
                                                 parent = transform,
                                                 worldSpace = true,
                                             });
                var cell = obj.GetComponentInChildren<ConstructionCell>();
                cells[fieldDislay.Position.Z, fieldDislay.Position.X] = cell;
                cell.Click.AddListener(() => OnCellClick(fieldDislay.Position));
            }
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        private void OnCellClick(GridPosition position)
        {
            Click?.Invoke(position);
        }
    }
}
