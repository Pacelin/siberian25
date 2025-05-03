using R3;
using UnityEngine;

namespace Siberian25.Game.Characters
{
    [System.Serializable]
    public class PlayerCombo
    {
        [SerializeField] private float _cooldown;
        
        private float _comboResetEstimation;

        private readonly ReactiveProperty<int> _value = new(0);

        public void Add(int count)
        {
            _value.Value += count;
            _comboResetEstimation = Time.time + _cooldown;
        }

        public void Update()
        {
            if (_value.Value == 0)
                return;
            if (Time.time >= _comboResetEstimation)
                _value.Value = 0;
        }
    }
}