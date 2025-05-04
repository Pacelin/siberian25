using DG.Tweening;
using UnityEngine;

namespace Siberian25.Game.Characters
{
    public class DebrisComponent : MonoBehaviour
    {
        private void OnDisable() => DOTween.Kill(this);

        public void Activate(float seconds)
        {
            var renderer = GetComponent<MeshRenderer>();
            if (renderer)
            {
                renderer.material.DOFade(0, seconds)
                    .SetTarget(this)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => Destroy(gameObject))
                    .Play();
                return;
            }

            var sp = GetComponent<SpriteRenderer>();
            if (sp)
            {
                sp.material.DOFade(0, seconds)
                    .SetTarget(this)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => Destroy(gameObject))
                    .Play();
            }
        }
    }
}