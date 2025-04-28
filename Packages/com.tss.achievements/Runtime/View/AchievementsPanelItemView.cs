using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TSS.Achievements.View
{
    public class AchievementsPanelItemView : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private Image _icon;
        [SerializeField] private float _applyFillDuration = 1f;
        [Space]
        [SerializeField] private TMP_Text _captionText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _percentText;
        [Space]
        [SerializeField] private UnityEvent _onLock;
        [SerializeField] private UnityEvent _onUnlock;
        [SerializeField] private UnityEvent _onSecretLock;
        [SerializeField] private UnityEvent _onSecretUnlock;
        [SerializeField] private UnityEvent _onLoadFinishedSuccessful;
        [SerializeField] private UnityEvent _onLoadFinishedFailure;

        private void OnDisable() => DOTween.Kill(this);

        public void SetInfo(AchievementConfig config)
        {
            _icon.sprite = config.Icon;
            _captionText.text = config.Caption;
            _descriptionText.text = config.Description;
        }

        public void SetLocked() => _onLock.Invoke();
        public void SetUnlocked() => _onUnlock.Invoke();
        public void SetSecretLocked() => _onSecretLock.Invoke();
        public void SetSecretUnlocked() => _onSecretUnlock.Invoke();
        
        public void SetLoadFinishedFailure() => _onLoadFinishedFailure.Invoke();
        public void SetLoadFinishedSuccess(float ratio)
        {
            _onLoadFinishedSuccessful.Invoke();
            _percentText.text = (ratio * 100).ToString("0.0");
            _fillImage.DOFillAmount(ratio, _applyFillDuration)
                .SetTarget(this)
                .Play();
        }
    }
}