#if UNITY_WEBGL && !UNITY_EDITOR
using System.Runtime.InteropServices;

namespace TSS.Utils.Saving
{
    internal class SavePrefsWebGL : ISavePrefs
    {
        private const string SAVE_PATH = "idbfs/some_unique_path/";
        
        public void SetString(string key, string data) => saveData(PrefixKey(key), data);
        public string GetString(string key, string defaultValue = "")
        {
            var result = loadData(PrefixKey(key));
            if (string.IsNullOrEmpty(result))
                return defaultValue;
            return result;
        }

        public void SetInt(string key, int data) => saveData(PrefixKey(key), data.ToString());
        public int GetInt(string key, int defaultValue = 0)
        {
            var str = GetString(key);
            if (int.TryParse(str, out var value))
                return value;
            return defaultValue;
        }

        public bool HasKey(string key)
        {
            var data = loadData(PrefixKey(key));
            return !string.IsNullOrEmpty(data);
        }

        public void DeleteKey(string key) => deleteKey(PrefixKey(key));
        public void ClearData() => deleteAllKeys(SAVE_PATH);
        public void Save() { }
        
        private static string PrefixKey(string key) => SAVE_PATH + key;
        
        [DllImport("__Internal")]
        private static extern void saveData(string key, string data);
        [DllImport("__Internal")]
        private static extern string loadData(string key);
        [DllImport("__Internal")]
        private static extern string deleteKey(string key);
        [DllImport("__Internal")]
        private static extern string deleteAllKeys(string prefix);
    }
}
#endif