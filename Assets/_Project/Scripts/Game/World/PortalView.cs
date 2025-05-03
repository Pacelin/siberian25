using Cysharp.Threading.Tasks;
using TSS.Tweening;
using UnityEngine;

namespace Siberian25.Game.World
{
    public class PortalView : MonoBehaviour
    {
        [SerializeField] private ScriptableTween _appearTween;
        [SerializeField] private ScriptableTween _disappearTween;
        
        public UniTask Activate()
        {
            _appearTween.Play();
            return _appearTween.WaitWhilePlay();
        }

        public UniTask Deactivate()
        {
            _disappearTween.Play();
            return _disappearTween.WaitWhilePlay();
        }
    }
}