using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLeavesAlpha : MonoBehaviour
{
    float minAlpha = 0.25f;

    MeshRenderer leavesRenderer;

    void Start()
    {
        leavesRenderer = GetComponent<MeshRenderer>();
    }

    void OnHitEnter()
    { 
        StartCoroutine(FadeOut(leavesRenderer.material));
    }

    void OnHitStay()
    {
        StartCoroutine(FadeOut(leavesRenderer.material));
    }

    void OnHitExit()
    {
        StartCoroutine(FadeIn(leavesRenderer.material));
    }

    IEnumerator FadeOut(Material mat)
    {
        while (mat.color.a > minAlpha)
        {
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a - 0.01f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator FadeIn(Material mat)
    {
        while (mat.color.a < 1)
        {
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a + 0.01f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

}
