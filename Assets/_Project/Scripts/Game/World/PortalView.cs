using Cysharp.Threading.Tasks;
using TSS.Tweening;
using UnityEngine;

namespace Siberian25.Game.World
{
    public class PortalView : MonoBehaviour
    {
        public bool IsActive { get; private set; }
        
        [SerializeField] private ScriptableTween _appearTween;
        [SerializeField] private ScriptableTween _disappearTween;
        
        public UniTask Activate()
        {
            IsActive = true;
            _appearTween.Play();
            return _appearTween.WaitWhilePlay();
        }

        public async UniTask Deactivate()
        {
            _disappearTween.Play();
            await _disappearTween.WaitWhilePlay();
            IsActive = false;
        }
    }
}