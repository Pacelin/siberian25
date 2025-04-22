namespace TSS.Utils.Saving
{
    public interface ISavePrefs
    {
        void SetString(string key, string data);
        string GetString(string key, string defaultValue = "");
        bool HasKey(string key);
        void DeleteKey(string key);
        void ClearData();
        void Save();
    }
}