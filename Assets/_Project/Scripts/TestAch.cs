using System;
using TSS.Achievements;
using UnityEngine;

namespace Siberian25
{
    public class TestAch : MonoBehaviour
    {
        [AchievementKey]
        [SerializeField] private string _ach1;
        [AchievementKey]
        [SerializeField] private string _ach2;
        [AchievementKey]
        [SerializeField] private string _ach3;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                Achievements.Report(_ach1);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                Achievements.Report(_ach2);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                Achievements.Report(_ach3);
        }
    }
}