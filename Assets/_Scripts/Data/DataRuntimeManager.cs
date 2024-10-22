using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataRuntimeManager : KBTemplate.Patterns.Singleton.Singleton<DataRuntimeManager>
{
    public static string DataPersistentDirectoryPath => Application.persistentDataPath + "/DT";
    public readonly static string PLAYER_DATA_RUNTIME_FILE_NAME = "PLR_DT.ngm";

    [SerializeField] private SaveGameSO defaultSaveGameFile;
    public DynamicData dynamicData { get; private set; }
    public override void OnCreatedSingleton()
    {
        base.OnCreatedSingleton();
        DontDestroyOnLoad(this);
        Init();
    }
    private void Init()
    {
        LoadPlayerDataRuntime();
    }

    #region dynamic data
    private void LoadPlayerDataRuntime()
    {
        dynamicData = SimpleDataSave.LoadData<DynamicData>(System.IO.Path.Combine(DataPersistentDirectoryPath, PLAYER_DATA_RUNTIME_FILE_NAME));
        if (dynamicData == null)
        {
            if (!defaultSaveGameFile)
                dynamicData = defaultSaveGameFile.dynamicData.DeepCopy();
            else
                dynamicData = new DynamicData();
        }
    }
    #endregion
    
    private void SaveDataRuntime()
    {
        SimpleDataSave.SaveData(dynamicData, PLAYER_DATA_RUNTIME_FILE_NAME, DataPersistentDirectoryPath);
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveDataRuntime();
        }
    }
}
