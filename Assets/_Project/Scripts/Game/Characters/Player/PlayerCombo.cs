using R3;

namespace Siberian25.Game.Characters
{
    [System.Serializable]
    public class PlayerCombo
    {
        public ReadOnlyReactiveProperty<int> Value => _value;

        private readonly ReactiveProperty<int> _value = new(0);

        public void Add(int count) => _value.Value += count;
        public void Reset() => _value.Value = 0;
    }
}