using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestAlgorithmA.Scipts.Runtimes;

namespace TestAlgorithmA.Scripts.Logic
{
    internal class Pointer : MonoBehaviour
    {
        [SerializeField] private Runtime currentRuntime = null;

        [SerializeField] private Runtime findPath = null;
        [SerializeField] private Runtime mapChanger = null;

        public Runtime CurrentRuntime
        {
            get { return currentRuntime; }
        }

        public void ChangeCurrentRuntime()
        {
            if (currentRuntime == findPath)
                currentRuntime = mapChanger;

            else
                currentRuntime = findPath;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    currentRuntime.Click(hit.collider.transform);
                }
            }

            currentRuntime.Run();
        }
    }
}
