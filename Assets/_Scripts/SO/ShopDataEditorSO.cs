using Sirenix.OdinInspector;
using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Edit Tools/Dynamic Data", fileName = "Dynamic Data", order = 0)]
public class DynamicDataSO : ScriptableObject
{
    public DynamicData dynamicData;

    [ButtonGroup("Data")]
    public void SaveUserData()
    {
        if (EditorUtility.DisplayDialog("Save Shop Data", "Are you sure you wanna save this save?", "100% Sure", "Not now"))
        {
            SimpleDataSave.SaveData(dynamicData, DataRuntimeManager.PLAYER_DATA_RUNTIME_FILE_NAME, DataRuntimeManager.DataPersistentDirectoryPath);
        }
    }
    [ButtonGroup("Data")]
    public void ReloadUserData()
    {
        dynamicData = SimpleDataSave.LoadData<DynamicData>(Path.Combine(DataRuntimeManager.DataPersistentDirectoryPath, DataRuntimeManager.PLAYER_DATA_RUNTIME_FILE_NAME));
    }
    [ButtonGroup("Data")]
    public void DeleteUserData()
    {
        if (EditorUtility.DisplayDialog("Delete Shop Data", "Are you sure you wanna delete this save?", "100% Sure", "Not now"))
        {
            SimpleDataSave.DeleteData<DynamicData>(DataRuntimeManager.PLAYER_DATA_RUNTIME_FILE_NAME, DataRuntimeManager.DataPersistentDirectoryPath);
            dynamicData = new DynamicData();
        }
    }
}
#endif