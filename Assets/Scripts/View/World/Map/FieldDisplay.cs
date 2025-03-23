#nullable enable
using UnityEngine;
using System;
using System.Collections;
using Safari.Model;
using System.Diagnostics;
using Safari.Model.Map;

namespace Safari.View.World.Map
{
    public class FieldDisplay : MonoBehaviour
    {
        public Field? Field { get; private set; }

        public GridPosition Position { get; private set; }

        private GameObject? displayed;

        [SerializeField]
        private FieldPrefabMapping mappings;

        public void Init(Field field, GridPosition position)
        {
            Position = position;
            Trace.Assert(displayed == null);
            DisplayField(field);
        }

        public void DisplayField(Field field)
        {
            Field = field;
            if (displayed != null)
            {
                Destroy(displayed);
            }
            var prefab = mappings.GetPrefab(field);
            if (prefab == null)
            {
                return;
            }
            displayed = Instantiate(prefab, transform, false);
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}