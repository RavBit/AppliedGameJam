using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BlinkEffect : MonoBehaviour {
    bool start = false;
    void OnEnable()
    {
        start = true;
        StartCoroutine("Blink");
    }
    void OnDisable()
    {
        start = false;
        StopCoroutine("Blink");
    }

    IEnumerator Blink()
    {
        while (start)
        {
            GetComponent<Text>().DOFade(1, 1);
            GetComponent<Image>().DOFade(1, 1);
            yield return new WaitForSeconds(1);
            GetComponent<Image>().DOFade(0, 1);
            GetComponent<Text>().DOFade(0, 1);
            yield return new WaitForSeconds(1);
        }
    }
}
