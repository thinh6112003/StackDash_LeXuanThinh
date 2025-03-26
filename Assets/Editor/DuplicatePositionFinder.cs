using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class DuplicatePositionFinder : EditorWindow
{
    // Danh sách chứa các vật thể trùng world position
    private static Dictionary<Vector3, List<GameObject>> positionDict = new Dictionary<Vector3, List<GameObject>>();

    // Biến để kiểm soát vị trí cuộn
    private Vector2 scrollPosition;

    [MenuItem("Tools/Find Duplicate World Positions")]
    public static void ShowWindow()
    {
        // Mở cửa sổ công cụ
        EditorWindow.GetWindow(typeof(DuplicatePositionFinder));
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Find Duplicate World Positions"))
        {
            FindDuplicatePositions();
        }

        GUILayout.Space(20);

        // Bắt đầu thanh cuộn dọc
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Width(position.width), GUILayout.Height(position.height - 60));

        // Hiển thị kết quả
        foreach (var entry in positionDict)
        {
            GUILayout.Label($"Position: {entry.Key}");
            foreach (var obj in entry.Value)
            {
                // Hiển thị nút "Go to Object"
                if (GUILayout.Button($"Go to {obj.name}"))
                {
                    SelectObjectInScene(obj);
                }
                GUILayout.Label($" - {obj.name}");
            }
        }

        // Kết thúc thanh cuộn
        EditorGUILayout.EndScrollView();
    }

    private static void FindDuplicatePositions()
    {
        positionDict.Clear(); // Xóa danh sách cũ

        // Lấy tất cả các GameObject trong scene hiện tại
        GameObject[] allGameObjects = Object.FindObjectsOfType<GameObject>();

        foreach (var go in allGameObjects)
        {
            // Chỉ xét những đối tượng đang hoạt động (trong scene) và tên chứa "CubeWall"
            if (go.activeInHierarchy && go.name.Contains("CubeWall"))
            {
                // Lấy worldPosition của vật thể
                Vector3 worldPos = go.transform.position;

                // Nếu position đã tồn tại trong từ điển, thêm vật thể vào danh sách
                if (positionDict.ContainsKey(worldPos))
                {
                    positionDict[worldPos].Add(go);
                }
                else
                {
                    // Nếu chưa tồn tại, tạo mới một danh sách
                    positionDict[worldPos] = new List<GameObject> { go };
                }
            }
        }

        // Hiển thị kết quả nếu có
        if (positionDict.Count == 0)
        {
            Debug.Log("Không có vật thể trùng world position hoặc không có đối tượng tên 'CubeWall'.");
        }
        else
        {
            Debug.Log("Đã tìm thấy các vật thể trùng world position có tên chứa 'CubeWall'.");
        }
    }

    private static void SelectObjectInScene(GameObject go)
    {
        // Chọn vật thể trong Scene view
        Selection.activeGameObject = go;
        // Đảm bảo rằng Scene view sẽ tập trung vào đối tượng đó
        SceneView.FrameLastActiveSceneView();
    }
}
