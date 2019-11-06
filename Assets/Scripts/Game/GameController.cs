using UnityEngine;
using Entitas;

namespace Samurai
{
    public class GameController : MonoBehaviour
    {
        private Systems _systems;
        private void Start()
        {
            _systems = new Feature("Systems");

            _systems.Initialize();
        }

        private void Update()
        {
            _systems.Execute();
            _systems.Cleanup();
        }

        private void OnDestroy()
        {
            _systems.TearDown();
        }
    }
}
