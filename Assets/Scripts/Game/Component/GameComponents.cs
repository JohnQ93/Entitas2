using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Samurai
{
    /// <summary>
    /// �������״̬�����ݴ�״̬�ĸı䲥���������
    /// </summary>
    [Game, Event(EventTarget.Self)]
    public class CameraState : IComponent
    {
        public CameraAniName state;
    }
}
