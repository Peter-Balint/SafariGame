using Safari.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Safari.View.World
{
    public class NavMeshBuilder : MonoBehaviour
    {
        public UnityEvent NavMeshBuilt;

        public bool ForceRebuildOnMapChange = false;

        [SerializeField]
        private NavMeshSurface navMesh;

        private float rebuildDelay = 0.5f;

        public async void BuildNavMesh()
        {
            await Awaiters.NextFrame;
            navMesh.BuildNavMesh();
            NavMeshBuilt.Invoke();
            if (ForceRebuildOnMapChange)
            {
                SafariGame.Instance.Map.FieldChanged += Map_FieldChanged; ;
            }
        }

        private void Map_FieldChanged(object sender, Model.Map.GridPosition e)
        {
            ScheduleNavMeshRebuild();
        }

        private Coroutine rebuildRoutine;

        void ScheduleNavMeshRebuild()
        {
            if (rebuildRoutine != null)
                StopCoroutine(rebuildRoutine);

            rebuildRoutine = StartCoroutine(DelayedRebuild());
        }

        IEnumerator DelayedRebuild()
        {
            yield return new WaitForSeconds(rebuildDelay);
            navMesh.UpdateNavMesh(navMesh.navMeshData);
        }
    }
}
