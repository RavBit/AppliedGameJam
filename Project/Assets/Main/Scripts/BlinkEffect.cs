using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BlinkEffect : MonoBehaviour {
    void Start()
    {
        StartCoroutine("Blink");
    }

    IEnumerator Blink()
    {
        while (true)
        {
            GetComponent<Image>().DOFade(0, 1);
            yield return new WaitForSeconds(1);
            GetComponent<Image>().DOFade(1, 1);
            yield return new WaitForSeconds(1);
        }
    }
}
