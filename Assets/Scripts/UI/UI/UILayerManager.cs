using System;
using UnityEngine;
using Const;
using System.Collections.Generic;

namespace UIFrame
{
    public class UILayerManager : MonoBehaviour
    {
        private readonly Dictionary<UILayer, Transform> _layerDictionary = new Dictionary<UILayer, Transform>();
        private void Awake()
        {
            Transform temp = null;
            foreach (UILayer layer in Enum.GetValues(typeof(UILayer)))
            {
                temp = transform.Find(layer.ToString());
                if(temp == null)
                {
                    Debug.LogError("can not find Layer: " + layer + "GameObject");
                    continue;
                }
                else
                {
                    _layerDictionary[layer] = temp;
                }
            }
        }

        public Transform GetLayerObject(UILayer layer)
        {
            if (_layerDictionary.ContainsKey(layer) && _layerDictionary[layer] != null)
            {
                return _layerDictionary[layer];
            }
            else
            {
                Debug.LogError("_layerDictionary did not contains layer: " + layer);
                return null;
            }
        }
    }
}
