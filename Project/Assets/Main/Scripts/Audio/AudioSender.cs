using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioSender {
	void SendAudio(AudioClip ac);
}

public class AudioSender : MonoBehaviour, IAudioSender {

	private AudioSourceStorage loopHost;

	private HealthState hs = HealthState.healthy;

	[Tooltip("This is the audioclip that is going to play.")]
	public AudioClip clip;

	[Tooltip("This bool determines whether or not the following 4 audio clips get used or not.")]
	public bool useHealth = false;
	public AudioClip healtyClip, illClip, skinnyClip, fatClip;

	[Tooltip("Check if the audiosource should play when object is set to active")]
	public bool playOnEnable = true;

	[Tooltip("Check if the audiosource should loop")]
	public bool isLoop = false;

	[Tooltip("The delay also keeps the duration of the current audioclip in mind.")]
	public float delay;

	private bool isRunning = true;

	public void OnEnable() {
		EventManager.UpdateHealth += CatchHealthState;
		if(clip == null && useHealth == false) {
			Debug.LogError("No clip implemented, not running!");
		}
		else if(useHealth == true && (healtyClip == null || illClip == null || skinnyClip == null || fatClip == null)) {
			Debug.LogError("One of the health clips not implemented, not running!");
		}
		else if(playOnEnable){
			isRunning = true;
			if(isLoop)
				StartCoroutine(PlayAudio());
			else
				_PlayAudio();
		}
	}
	public void OnDisable() {
		EventManager.UpdateHealth -= CatchHealthState;
		isRunning = false;
		StopCoroutine(PlayAudio());
	}

	private AudioClip GetClip() {
		if(useHealth) {
			switch(hs) {
				case HealthState.healthy:
					return healtyClip;
				case HealthState.fat:
					return fatClip;
				case HealthState.skinny:
					return skinnyClip;
				case HealthState.ill:
					return illClip;
				default:
					return clip;
			}
		}
		else {
			return clip;
		}
	}

	public void _PlayAudio() {
		SendAudio(GetClip());
	}

	private void CatchHealthState(HealthState hs) {
		this.hs = hs;
	}

	public IEnumerator PlayAudio() {
		loopHost.Lock();
		while(isRunning) {
			yield return new WaitForSeconds(delay + GetClip().length);
			loopHost.PlayAudioclip(GetClip());
		}
		loopHost.Unlock();
	}

	public void SendAudio(AudioClip ac) {
		EventManager._SendAudio(ac);
	}
}
