using JetBrains.Annotations;
using Newtonsoft.Json;

namespace TSS.Utils.Saving
{
    [PublicAPI]
    public static class SaveSystem
    {
        private static ISavePrefs _savePrefs;

        static SaveSystem()
        {
#if UNITY_EDITOR
            _savePrefs = new SavePrefsPlayerPrefs();
#elif UNITY_WEBGL
            _savePrefs = new SavePrefsWebGL();
#else
            _savePrefs = new SavePrefsPlayerPrefs();
#endif
        }

        public static void Save<T>(string key, T data) =>
            _savePrefs.SetString(key, JsonConvert.SerializeObject(data));
        
        public static T Load<T>(string key, T defaultValue = default)
        {
            if (_savePrefs.HasKey(key))
                return JsonConvert.DeserializeObject<T>(_savePrefs.GetString(key));
            return defaultValue;
        }
    }
}