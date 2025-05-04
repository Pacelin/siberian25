using R3;
using TSS.Achievements;
using UnityEngine;

namespace Siberian25.Game.Characters
{
    [System.Serializable]
    public class PlayerCombo
    {
        public ReadOnlyReactiveProperty<int> Value => _value;

        private readonly ReactiveProperty<int> _value = new(0);

        private int _maxCombo;

        public void Add(int count)
        {
            _value.Value += count;

            _maxCombo = Mathf.Max(_value.Value, _maxCombo);

            switch (_maxCombo)
            {
                case >= 500:
                    Achievements.Report("Ach7");
                    break;
                case >= 250:
                    Achievements.Report("Ach6");
                    break;
                case >= 100:
                    Achievements.Report("Ach5");
                    break;
                case >= 25:
                    Achievements.Report("Ach1");
                    break;
            }
        }

        public void Reset() => _value.Value = 0;
    }
}