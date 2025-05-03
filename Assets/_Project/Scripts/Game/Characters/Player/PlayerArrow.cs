using TSS.Core;
using UnityEngine;

namespace Siberian25.Game.Characters
{
    public class PlayerArrow : MonoBehaviour
    {
        [SerializeField] private float _x = 60;

        private void Update()
        {
            if (Runtime.IsPaused)
                return;
            var vector = PlayerPointer.GetVector(PlayerPointer.GetPosition());
            var angle = Vector2.SignedAngle(Vector2.left, vector);
            transform.rotation = Quaternion.Euler(_x, 0, angle);
        }
    }
}