using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Abstract
{
    internal abstract class ClickableBase : MonoBehaviour
    {
        private protected Button _button;
        private protected GameObject _buttonGameObject;

        private void Start()
        {
            _button = GetComponent<Button>();
            _buttonGameObject = _button.gameObject;
            _button.onClick.AddListener(OnClick);
        }

        private protected abstract void OnClick();
    }
}