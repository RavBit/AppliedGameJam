using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class NewsManager : MonoBehaviour {
    public GameObject scrollitem;
	public Text newstext;
    public Transform startpos;
    public AudioSource BM;
    public bool scrolling = true;
	// Use this for initialization
	void Start () {
        EventManager.SendBreakingNews += StartScrol;
        //StartCoroutine("Scrollnews");
    }
    private void StartScrol(string s)
    {
        BM.Play();
        s = "";
        scrolling = true;
    }


	public IEnumerator Scrollnews()
    {
		
		int l = newstext.text.Length < 20 ? 25 : newstext.text.Length;
		while(true) {
			Sequence seq = DOTween.Sequence();
			seq.Append(scrollitem.transform.DOLocalMoveX(-l / 2, (l * .2f)));
            yield return new WaitForSeconds((l * .2f) - .5f);
			seq.Kill();
            Debug.Log("scrolling back");
            scrollitem.transform.localPosition = new Vector3(4, 0, 0);
        }
		
        //scrollitem.transform.position = startpos.position;
    }

	public string MultiplyText(string s, int num) {
		string temp = "";
		for(int i = 0; i < 5; i++) {
			temp += s;
		}
		return temp;
	}
}
