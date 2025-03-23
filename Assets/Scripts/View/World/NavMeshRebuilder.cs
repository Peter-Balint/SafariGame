using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;

namespace Safari.View.World
{
    public class NavMeshRebuilder : MonoBehaviour
    {
        [SerializeField]
        private NavMeshSurface navMesh;
        public async void RebuildNavMesh()
        {
            await Awaiters.NextFrame;
            navMesh.BuildNavMesh();
        }
    }
}
