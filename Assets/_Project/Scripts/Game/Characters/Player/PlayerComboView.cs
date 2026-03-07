using System;
using R3;
using TMPro;
using TSS.Tweening;
using UnityEngine;

namespace Siberian25.Game.Characters
{
    public class PlayerComboView : MonoBehaviour
    {
        [SerializeField] private ScriptableTween _appearTween;
        [SerializeField] private ScriptableTween _superAppearTween;
        [SerializeField] private ScriptableTween _pingTween;
        [SerializeField] private ScriptableTween _superPingTween;
        [SerializeField] private ScriptableTween _disappearTween;
        [Space]
        [SerializeField] private TMP_Text _text;
        [SerializeField] private string _format;

        private IDisposable _healthDisposable;
        private IDisposable _disposable;
        private bool _appeared;
        private bool _superAppeared;
        
        private void OnEnable()
        {
            _healthDisposable = GameContext.Player.Health.OnDamage.Subscribe(_ => GameContext.Player.Combo.Reset());
            _disposable = GameContext.Player.Combo.Value.Subscribe(amount =>
            {
                if (amount is > 0 and < 10 && !_appeared)
                {
                    _text.text = string.Format(_format, amount);
                    if (_disappearTween.IsPlaying)
                        _disappearTween.Pause();
                    _appearTween.Play();
                    _appeared = true;
                    return;
                }
                if (amount >= 10 && !_superAppeared)
                {
                    _text.text = string.Format(_format, amount);
                    if (_disappearTween.IsPlaying)
                        _disappearTween.Pause();
                    if (_appearTween.IsPlaying)
                        _appearTween.Pause();
                    _superAppearTween.Play();
                    _appeared = true;
                    return;
                }

                if (amount == 0)
                {
                    if (_appearTween.IsPlaying)
                        _appearTween.Pause();
                    if (_superAppearTween.IsPlaying)
                        _superAppearTween.Pause();
                    _disappearTween.Play();
                    _appeared = false;
                    _superAppeared = false;
                    return;
                }
                
                _text.text = string.Format(_format, amount);
                if (amount < 10)
                    _pingTween.Play();
                else
                    _superPingTween.Play();
            });
        }

        private void OnDisable()
        {
            _healthDisposable?.Dispose();
            _disposable?.Dispose();
        }
    }
}