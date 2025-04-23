using TSS.Utils;
using UnityEngine;

namespace TSS.Supabase
{
    [CreateSingletonAsset("Assets/TSS/Supabase Settings.asset", "Supabase Settings")]
    public class SupabaseSettings : ScriptableObject
    {
        public string SupabaseURL => _supabaseURL;
        public string SupabaseAnonKey => _supabaseAnonKey;
        
        [SerializeField] private string _supabaseURL;
        [SerializeField] private string _supabaseAnonKey;
    }
}