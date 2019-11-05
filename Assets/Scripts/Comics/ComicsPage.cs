using UnityEngine;
using UnityEngine.UI;
using Util;

namespace UIFrame
{
    public class ComicsPage : MonoBehaviour
    {
        private Sprite[] sprites;
        private Image _indexImage;
        void Start()
        {
            sprites = GetComponent<NumSprites>().sprites;
            _indexImage = transform.Find("Index").Image();
        }

        public void ShowNum(int index)
        {
            if(index >= sprites.Length)
            {
                Debug.LogError("index out of range");
                return;
            }
            else
            {
                _indexImage.sprite = sprites[index];
            }
        }
    }
}
