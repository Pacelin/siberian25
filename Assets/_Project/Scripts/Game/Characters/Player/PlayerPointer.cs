using TSS.Core;
using UnityEngine;

namespace Siberian25.Game.Characters
{
    public static class PlayerPointer
    {
        public static Vector2 GetVector(Vector2 position) => position - GameContext.Player.Rigidbody.position;
        public static Vector2 GetPosition() => SceneCameraProvider.MainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}