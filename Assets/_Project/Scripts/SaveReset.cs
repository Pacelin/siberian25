using System;
using R3;
using Siberian25.UI.Common;
using TSS.Utils.Saving;
using UnityEngine;

namespace Siberian25
{
    public class SaveReset : MonoBehaviour
    {
        [SerializeField] private ScriptableButton _button;

        private IDisposable _disposable;
        
        private void OnEnable()
        {
            _disposable = _button.ObserveClick().Subscribe(_ =>
            {
                SaveSystem.ClearSaves();
            });
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}
