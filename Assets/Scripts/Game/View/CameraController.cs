using UnityEngine;
using Entitas;
using Entitas.Unity;

namespace Samurai
{
    public class CameraController : MonoBehaviour, IInitializeSystem, ISamuraiCameraStateListener
    {
        public void Initialize()
        {
            GameEntity entity = Contexts.sharedInstance.game.CreateEntity();
            gameObject.Link(entity);
            entity.AddSamuraiCameraStateListener(this);
        }

        public void OnSamuraiCameraState(GameEntity entity, CameraAniName state)
        {
            throw new System.NotImplementedException();
        }
    }
}
