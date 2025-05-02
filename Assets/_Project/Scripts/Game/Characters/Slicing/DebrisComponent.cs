using DG.Tweening;
using UnityEngine;

namespace Siberian25.Game.Characters
{
    public class DebrisComponent : MonoBehaviour
    {
        private void OnDisable() => DOTween.Kill(this);

        public void Activate(float seconds)
        {
            transform.DOScale(0, seconds)
                .SetTarget(this)
                .SetEase(Ease.Linear)
                .OnComplete(() => Destroy(gameObject))
                .Play();
        }
    }
}