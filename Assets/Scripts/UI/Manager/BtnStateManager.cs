using System.Collections.Generic;
using UnityEngine;
using Util;
using DG.Tweening;
using System.Linq;
using System;

namespace UIFrame
{
    public class BtnStateManager : MonoBehaviour
    {
        private List<BtnParent> _currentParents = new List<BtnParent>();
        private int _parentId;

        public void InitBtnParent(List<Transform> parents)
        {
            if (parents == null)
                return;
            BtnParent temp;
            for (int i = 0; i < parents.Count; i++)
            {
                temp = parents[i].gameObject.AddComponent<BtnParent>();
                temp.Init(i);
            }
        }

        private bool JudgeException(List<Transform> parents)
        {
            return parents == null || parents.Count == 0;
        }

        public void SetDefaultBtn(List<BtnParent> parents)
        {
            foreach (BtnParent parent in parents)
            {
                if (parent.Index == 0)
                {
                    parent.SelectedDefault();
                }
            }
        }

        public void Show(Transform showUI)
        {
            ResetBtnState();
            ResetData();
            _currentParents = showUI.GetComponentsInChildren<BtnParent>(true).ToList();
            SetDefaultBtn(_currentParents);
        }

        public void Hide(Transform hideUI)
        {
            if (hideUI == null)
                return;
            var hideParents = hideUI.GetComponentsInChildren<BtnParent>(true).ToList();
            foreach (BtnParent parent in hideParents)
            {
                parent.SelectedState = Const.SelectedState.UNSELECTED;
            }
        }

        public void SelectedButton()
        {
            _currentParents[_parentId].SelectedButton();
        }

        private void ResetData()
        {
            _parentId = 0;
            _currentParents.Clear();
        }

        private void ResetBtnState()
        {
            foreach (BtnParent btnParent in _currentParents)
            {
                btnParent.ResetChild();
            }
        }

        public void Left()
        {
            MoveIndex(_currentParents[_parentId].Left, -1);
        }

        public void Right()
        {
            MoveIndex(_currentParents[_parentId].Right, 1);
        }

        private bool MoveIndex(Func<bool> moveAction, int symbol)
        {
            if (JudgeException(moveAction, symbol))
                return false;

            if (_parentId >= 0 && _parentId < _currentParents.Count)
            {
                if (moveAction())
                {
                    _currentParents[_parentId].SelectedState = Const.SelectedState.SELECTED;
                    return true;
                }
                else
                {
                    _currentParents[_parentId].SelectedState = Const.SelectedState.UNSELECTED;
                    _parentId += symbol;
                    return MoveIndex(moveAction, symbol);
                }
            }
            else
            {
                ResetParentId();
                _currentParents[_parentId].SelectedState = Const.SelectedState.SELECTED;
                return true;
            }
        }

        private bool JudgeException(Func<bool> moveAction, int symbol)
        {
            if(moveAction == null)
            {
                Debug.LogError("moveAction is null");
                return true;
            }

            if(symbol != 1 && symbol != -1)
            {
                Debug.LogError("symbol must be 1 or -1");
                return true;
            }

            return false;
        }

        private void ResetParentId()
        {
            if (_parentId < 0)
            {
                _parentId = 0;
                return;
            }
            else if (_parentId >= _currentParents.Count)
            {
                _parentId = _currentParents.Count - 1;
            }
        }
    }
}
