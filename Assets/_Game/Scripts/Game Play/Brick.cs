using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] float param;
    private Tween tween;
    bool check = false;
    Coroutine t;
    public void SetMaterialAfter(Material material, float time)
    {
        StartCoroutine(SetMaterialAfterIE(material, time));
    }
    private IEnumerator SetMaterialAfterIE( Material material, float time)
    {
        SetZoomLayEffectAfter(0.1f);
        yield return new WaitForSeconds(time);
        //meshRenderer.material = material;
        meshRenderer.sharedMaterial = material;
    }
    public void SetZoomLayEffectAfter(float time)
    {
        t = StartCoroutine(SetZoomEffectAfterIE(time, 1.4f, 0.4f));
    }
    public void SetZoomEffectAfter(float time)
    {
        t = StartCoroutine(SetZoomEffectAfterIE(time,1.25f,0.05f));
    }
    public IEnumerator SetZoomEffectAfterIE(float time, float scale, float scaleCale)
    {
        yield return new WaitForSeconds(time);
        if (check == false) {
            check = true;
            tween = transform.DOScale(Vector3.one * scale, scaleCale).SetEase(Ease.OutCubic).
            OnComplete(() => {
                transform.localScale = Vector3.one;
                tween = transform.DOScale(Vector3.one, scaleCale).SetEase(Ease.OutCubic).OnComplete(() => {
                    transform.localScale = Vector3.one;
                });
            });
            yield return new WaitForSeconds(scaleCale*2f+0.1f);
            check = false;
        }
    }
}
