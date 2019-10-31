using UnityEngine;

namespace UIFrame
{
    public class InputManager : MonoBehaviour
    {
        void Start()
        {
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                RootManager.Instance.Back();
            }

            if (Input.GetMouseButtonDown(0))
            {
                RootManager.Instance.PlayAudio(Const.UIAudioName.UI_click);
            }

            BtnSelected();
        }

        private void BtnSelected()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                RootManager.Instance.ButtonLeft();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                RootManager.Instance.ButtonRight();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                RootManager.Instance.SelectedButton();
            }
        }
    }
}
