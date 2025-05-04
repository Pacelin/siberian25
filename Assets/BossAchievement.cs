using Siberian25.Game;
using TSS.Achievements;
using UnityEngine;

public class BossAchievement : MonoBehaviour
{
    private void Update()
    {
        if (GameContext.Boss != null && GameContext.Boss.MiddleHead.IsAlive)
            Achievements.Report("Ach4");
    }
}
