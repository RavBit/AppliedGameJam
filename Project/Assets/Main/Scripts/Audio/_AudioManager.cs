using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _AudioManager : MonoBehaviour {

	public List<AudioSourceStorage> sources;
	public List<AudioSourceStorage> loopSources;

	private void Awake() {
		DontDestroyOnLoad(this);
		sources = new List<AudioSourceStorage>();
		EventManager.SubmitAudioSource += CatchAudioSource;
	}

	private void CatchAudioSource(AudioSourceStorage a) {
		loopSources.Add(a);
	}

	private void RemoveAudioSource(AudioSourceStorage a) {
		for(int i = 0; i < sources.Count; i++) {
			if(sources[i] == a)
				sources[i] = null;
			if(loopSources[i] == a)
				loopSources[i] = null;
		}
	}

	private void CreateNewSource() {
		sources.Add(new AudioSourceStorage(gameObject.AddComponent<AudioSource>(), gameObject));
	}

	private void PlaySound(AudioClip ac) {
		foreach(AudioSourceStorage a in sources) {
			if(a.IsUnlocked()) {
				a.PlayAudioclip(ac);
				return;
			}
		}
		CreateNewSource();
		sources[sources.Count - 1].PlayAudioclip(ac);
	}
}

public enum LockState {
	locked, unlocked
}

public class AudioSourceStorage {
	private AudioSource source;
	public LockState lockState = LockState.unlocked;
	private GameObject parent;

	public AudioSourceStorage(AudioSource aS, GameObject p) {
		parent = p;
		source = aS;
	}

	public void Lock() {
		lockState = LockState.locked;
	}

	public void Unlock() {
		lockState = LockState.unlocked;
	}

	//returns true if unlocked
	public bool IsUnlocked() {
		lockState = source.isPlaying ? LockState.locked : LockState.unlocked;
		return lockState == LockState.unlocked ? true : false;
	}

	public void PlayAudioclip(AudioClip ac) {
		source.clip = ac;
		source.Play();
	}
}
