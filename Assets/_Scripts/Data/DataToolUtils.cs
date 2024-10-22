using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public static class DataToolUtils
{
    [MenuItem("Tools/Reset All Runtime Data")]
    public static void ResetAllData()
    {
        if (EditorUtility.DisplayDialog("Delete Runtime Data", "Are you sure you wanna delete this save?", "100% Sure", "Not now"))
        {
            SimpleDataSave.DeleteData<DynamicData>(DataRuntimeManager.PLAYER_DATA_RUNTIME_FILE_NAME, DataRuntimeManager.DataPersistentDirectoryPath);
            PlayerPrefs.DeleteAll();
        }
    }
}
#endif
