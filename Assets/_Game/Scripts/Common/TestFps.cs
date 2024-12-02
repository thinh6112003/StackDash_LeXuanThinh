using UnityEngine;
using TMPro;
public class TestFps : MonoBehaviour
{
    float timer = 0;
    float max = 0;
    float min = 100;
    float tong = 0;
    float dem = 0;
    float tongEnd = 0;
    float demEnd = 0;
    private void Update()
    {
        timer += Time.deltaTime;
        max = Mathf.Max(max, 1f / Time.deltaTime);
        min = Mathf.Min(min, 1f / Time.deltaTime);
        tong += 1f / Time.deltaTime;
        dem += 1f;

        tongEnd += 1f / Time.deltaTime;
        demEnd += 1f;
        if (timer > 1)
        {
            Debug.Log("=======");
            Debug.Log("Max fps: " + max);
            Debug.Log("Min fps: " + min);
            Debug.Log("Trung binh fps: " + tong / dem);
            timer = 0;
            tong = 0;
            max = 0;
            min = 100;
            dem = 0;
        }
    }
    private void OnDisable()
    {
        Debug.Log("End Game tb Fps: " + tongEnd / demEnd);
    }
}