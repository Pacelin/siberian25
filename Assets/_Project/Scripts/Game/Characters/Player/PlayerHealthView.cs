using System;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Siberian25.Game.Characters
{
    public class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private Image[] _hearts;

        private IDisposable _disposable;
        
        private void OnEnable()
        {
            _disposable = GameContext.Player.Health.OnHealthChanged.Subscribe(h =>
            {
                var h1 = Mathf.Clamp01(h / 2f);
                var h2 = Mathf.Clamp01((h - 2) / 2f);
                var h3 = Mathf.Clamp01((h - 4) / 2f);
                _hearts[0].fillAmount = h1;
                _hearts[1].fillAmount = h2;
                _hearts[2].fillAmount = h3;
            });
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}