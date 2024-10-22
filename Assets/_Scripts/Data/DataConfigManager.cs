using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using JetBrains.Annotations;

public class DataConfigManager : KBTemplate.Patterns.Singleton.Singleton<DataConfigManager>
{
    [Header("Jsons")]
    public TextAsset levelDataConfigJson;

    private List<LevelDataConfig> levelDataConfigs = new List<LevelDataConfig>();
    public override void OnCreatedSingleton()
    {
        base.OnCreatedSingleton();
        DontDestroyOnLoad(this);
        Init();
    }
    private void Init()
    {
        LoadLevelDataConfig();
    }

    #region level config
    private void LoadLevelDataConfig()
    {
        JsonSerializerSettings setting = new JsonSerializerSettings();
        setting.NullValueHandling = NullValueHandling.Ignore;
        levelDataConfigs = JsonConvert.DeserializeObject<List<LevelDataConfig>>(levelDataConfigJson.ToString(), setting);
    }
    public LevelDataConfig GetLeveDataConfig(int level)
    {
        return levelDataConfigs.Where(a => a.level == level).FirstOrDefault();
    }
    #endregion

}

[System.Serializable]
public class LevelDataConfig
{
    public int level = 0;
    public int expToLevelUp = 0;
}

