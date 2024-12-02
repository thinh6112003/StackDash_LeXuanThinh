using UnityEditor;
using UnityEngine;
using System.IO;

public class ConvertToLitURP : EditorWindow
{ 
    // Khai báo thư mục gốc chứa các file material
    private string folderPath = "Assets/Materials";  // Bạn có thể thay đổi đường dẫn này

    [MenuItem("Tools/Convert All URP Materials to URP Lit Shader")]
    public static void ShowWindow()
    {
        // Mở cửa sổ công cụ editor
        GetWindow<ConvertToLitURP>("Shader Conversion Tool");
    }

    private void OnGUI()
    {
        GUILayout.Label("Convert All URP Materials to URP Lit Shader", EditorStyles.boldLabel);

        // Chọn thư mục chứa material
        folderPath = EditorGUILayout.TextField("Folder Path", folderPath);

        if (GUILayout.Button("Convert URP Materials"))
        {
            ConvertURPToURPLit(folderPath);
        }
    }

    private void ConvertURPToURPLit(string path)
    {
        // Lấy tất cả các asset trong thư mục với đuôi .mat
        string[] materialGUIDs = AssetDatabase.FindAssets("t:Material", new[] { path });

        foreach (string guid in materialGUIDs)
        {
            string materialPath = AssetDatabase.GUIDToAssetPath(guid);
            Material material = AssetDatabase.LoadAssetAtPath<Material>(materialPath);

            if (material != null)
            {
                // Kiểm tra xem shader hiện tại của material có phải là URP không
                if (material.shader.name.Contains("Standard"))
                {
                    // Chỉ thay đổi shader nếu nó là shader của URP
                    if (material.shader != Shader.Find("Universal Render Pipeline/Lit"))
                    {
                        Undo.RecordObject(material, "Convert Shader to URP Lit");
                        material.shader = Shader.Find("Universal Render Pipeline/Lit");
                        EditorUtility.SetDirty(material);
                        Debug.Log($"Converted shader for material: {material.name}");
                    }
                }
            }
        }

        // Cập nhật lại database asset
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Shader conversion completed.");
    }
}
