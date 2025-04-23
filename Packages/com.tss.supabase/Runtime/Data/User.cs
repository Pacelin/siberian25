using Postgrest.Attributes;
using Postgrest.Models;

namespace TSS.Supabase.Data
{
    [Table("users")]
    public class User : BaseModel
    {
        [PrimaryKey("id", true)]
        public string UserId { get; set; }

        public User() => UserId = SupabaseManager.CurrentUserId;
    }
}