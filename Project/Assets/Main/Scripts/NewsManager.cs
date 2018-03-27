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
        StartCoroutine("Scrollnews");
    }
    private void StartScrol(string s)
    {
        BM.Play();
        s = "";
        scrolling = true;
    }
    public IEnumerator Scrollnews()
    {
        while (true)
        {
            scrollitem.transform.DOLocalMoveX(-15, 15);
            yield return new WaitForSeconds(15);
            Debug.Log("scrolling back");
            scrollitem.transform.localPosition = new Vector3(50, 0, 0);
        }

        //scrollitem.transform.position = startpos.position;
    }
}
