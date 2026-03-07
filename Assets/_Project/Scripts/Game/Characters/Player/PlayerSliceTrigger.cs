using System.Collections.Generic;
using TSS.Audio;
using UnityEngine;

namespace Siberian25.Game.Characters
{
    public class PlayerSliceTrigger : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;

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
            }
            _slicables.Clear();
        }

        public void DoTrigger()
        {
            var result = new List<Collider2D>();
            _collider.Overlap(new ContactFilter2D(), result);
            foreach (var col in result)
                OnTriggerEnter2D(col);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!EnableSlice)
                return;
            if (other.TryGetComponent<ISlicable>(out var slicable))
            {
                _wasSliceCurrentDash = true;
                slicable.OnSlice(transform.position,SliceDirection);
                AudioSystem.Game_Cut.PlayOneShot();
                _slicables.Add(slicable);
            }
        }
    }
}