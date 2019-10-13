using System;
using UnityEngine;
using UnityEngine.UI;

namespace Util
{
    /// <summary>
    /// 拓展方法工具类
    /// </summary>
    public static class ExtendUtil
    {
        public static void AddBtnListener(this RectTransform rect, Action action)
        {
            var button = rect.GetComponent<Button>() ?? rect.gameObject.AddComponent<Button>();

            button.onClick.AddListener(() => action());
        }

        public static RectTransform RectTransform(this Transform transform)
        {
            var rect = transform.GetComponent<RectTransform>();

            if(rect != null)
            {
                return rect;
            }
            else
            {
                Debug.LogError("can not find RectTransform");
                return null;
            }
        }
    }
}
