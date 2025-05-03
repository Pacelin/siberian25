using TMPro;
using TSS.Tweening;
using UnityEngine;

namespace Siberian25.Game.Characters
{
    public class PlayerCombeView : MonoBehaviour
    {
        [SerializeField] private ScriptableTween _appearTween;
        [SerializeField] private ScriptableTween _superTween;
        [SerializeField] private ScriptableTween _pingTween;
        [SerializeField] private ScriptableTween _disappearTween;
        [Space]
        [SerializeField] private TMP_Text _text;
        [SerializeField] private string _format;
        
        
    }
}