using TSS.Audio;
using UnityEngine;

namespace TSS.Achievements
{
    [CreateAssetMenu(menuName = "TSS/Achievement", fileName = "SO_Achievement")]
    public class AchievementConfig : ScriptableObject
    {
        public Sprite Icon => _icon;
        public string Caption => _caption;
        public string Description => _description;
        public int ReportsCount => _reportsCount;

        public SoundEvent ClaimSound => _claimSound;
        public EAchievementNotificationContainer NotificationContainer => _notificationContainer;

        [Header("Settings")] 
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _caption = "Caption";
        [SerializeField] private string _description = "Description";
        [SerializeField] private int _reportsCount = 1;
        [Header("View")]
        [SerializeField] private SoundEvent _claimSound;
        [SerializeField] private EAchievementNotificationContainer _notificationContainer;
    }
}