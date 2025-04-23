using Postgrest.Attributes;
using Postgrest.Models;
using TSS.Supabase;

namespace TSS.Achievements.Database
{
    [Table("users_achievements")] 
    public class UserAchievement : BaseModel
    {
        [PrimaryKey("id", false)]
        public int RowId { get; set; }
        [Column("achievement_id")]
        public string AchievementId { get; set; }
        [Column("user_id")]
        public string UserId { get; set; }

        public UserAchievement() { }
        public UserAchievement(string achievementId)
        {
            AchievementId = achievementId;
            UserId = SupabaseManager.CurrentUserId;
        }
    }
}
