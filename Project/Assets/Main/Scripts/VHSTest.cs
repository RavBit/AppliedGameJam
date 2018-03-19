using UnityEngine;
using System.Collections;

public class VHSTest : MonoBehaviour
{
    public Material _material;

    void Awake()
    {
        _material.SetFloat("_OffsetPosY", 0f);
        _material.SetFloat("_OffsetColor", 0f);
        _material.SetFloat("_OffsetDistortion", 750f);
        StartCoroutine("Test");
    }

    public IEnumerator Test()
    {
        while(true)
        {
            _material.SetFloat("_OffsetDistortion", 10f);
            yield return new WaitForSeconds(0.1f);
            _material.SetFloat("_OffsetDistortion", 1500f);
            yield return new WaitForSeconds(10);
        }
    }
}