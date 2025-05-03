using TSS.Core;
using UnityEngine;

namespace Siberian25.Game.Characters
{
    public static class PlayerPointer
    {
        public static Vector2 GetVector(Vector2 position) => position - (Vector2) GameContext.Player.DashTrail.transform.position;
        public static Vector2 GetPosition() => SceneCameraProvider.MainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}