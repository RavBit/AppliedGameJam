using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewsManager : MonoBehaviour {
    public GameObject scrollitem;
    public Transform startpos;
    public AudioSource BM;
    public bool scrolling = true;
	// Use this for initialization
	void Start () {
        EventManager.SendBreakingNews += StartScrol;
    }
    private void StartScrol(string s)
    {
        BM.Play();
        s = "";
        scrolling = true;
        StartCoroutine("Scrollnews");
    }
    public IEnumerator Scrollnews()
    {
        while (scrolling)
        {
            scrollitem.transform.DOLocalMoveX(-15, 15);
            yield return new WaitForSeconds(15);
            scrollitem.transform.localPosition = new Vector3(50, 0, 0);
        }

        scrollitem.transform.position = startpos.position;
    }

    public void StopScrolling()
    {
        scrolling = false;
        StopCoroutine("Scrollnews");
    }
}
