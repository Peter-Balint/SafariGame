#nullable enable
using UnityEngine;
using System;
using System.Collections;
using Safari.Model;
using System.Diagnostics;

namespace Safari.View.World
{
    public class FieldDisplay : MonoBehaviour
    {
        public Field Field { get; private set; }

        public GridPosition Position { get; private set; }

        private GameObject? displayed;

        public void Init(Field field, GridPosition position)
        {
            Field = field;
            Position = position;
            Trace.Assert(displayed == null);

        }

        void Start()
        {
        
        }

        void Update()
        {

        }
    }
}