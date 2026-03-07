using UnityEngine;

namespace Siberian25.Game.Characters
{
    public interface ISlicable
    {
        void OnSlice(Vector2 point, Vector2 direction);
        void OnSlicePerform(Vector2 point, Vector2 direction);
    }
}