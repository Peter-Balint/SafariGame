using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.Events;

namespace Safari.View.World
{
    public class NavMeshBuilder : MonoBehaviour
    {
        public UnityEvent NavMeshBuilt;

        [SerializeField]
        private NavMeshSurface navMesh;

        public async void BuildNavMesh()
        {
            await Awaiters.NextFrame;
            navMesh.BuildNavMesh();
            NavMeshBuilt.Invoke();
        }
    }
}
