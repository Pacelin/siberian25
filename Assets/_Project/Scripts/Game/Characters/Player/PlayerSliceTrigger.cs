using System.Collections.Generic;
using TSS.Audio;
using UnityEngine;

namespace Siberian25.Game.Characters
{
    public class PlayerSliceTrigger : MonoBehaviour
    {
        public Vector2 SliceDirection { get; set; }
        public bool EnableSlice { get; set; }

        private List<ISlicable> _slicables = new();
        private bool _wasSliceCurrentDash;

        public void StartDash() => _wasSliceCurrentDash = false;
        public bool WasSliceCurrentDash() => _wasSliceCurrentDash;
        
        public void PerformSlice()
        {
            foreach (var slicable in _slicables) 
                if (slicable != null)
                    slicable.OnSlicePerform(transform.position, SliceDirection);
            if (_slicables.Count > 0)
            {
                GameContext.Player.Combo.Add(_slicables.Count);
                AudioSystem.Game_Cut.PlayOneShot();
            }
            _slicables.Clear();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!EnableSlice)
                return;
            if (other.TryGetComponent<ISlicable>(out var slicable))
            {
                _wasSliceCurrentDash = true;
                slicable.OnSlice(transform.position,SliceDirection);
                _slicables.Add(slicable);
            }
        }
    }
}