using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour 
{
	public AudioSource music, voice;
	public GameObject soundPrefab;
	public AudioClip clickSound;

	[HideInInspector]
	public AudioClip[] clips;
	[HideInInspector]
	public AudioClip mainTheme;
    [HideInInspector]
    public AudioClip buffer;

	public static SoundManager Instance;

	private void Awake() 
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else Destroy(this.gameObject);
	}

	public void Say(AudioClip clip)
	{
		if (!voice.isPlaying)
		{
			voice.clip = clip;
			voice.Play();
		}
	}

	public void PlayClip(AudioClip clip, float volume = 1f, float pitch = 1f)
	{
		GameObject tmp = Instantiate(soundPrefab) as GameObject;
		tmp.name = clip.ToString();
		tmp.GetComponent<AudioSource>().clip = clip;
		tmp.GetComponent<AudioSource>().volume = volume;
		tmp.GetComponent<AudioSource>().pitch = pitch;
		tmp.GetComponent<AudioSource>().Play();
		tmp.transform.parent = this.transform;
		Destroy(tmp, clip.length);
	}

	public void PlayRandomClip(AudioClip[] clips)
	{
		PlayClip(clips[UnityEngine.Random.Range(0, clips.Length)]);
	}


	public void Click()
	{
		PlayClip(clickSound, 0.4F);
	}

	public void MusicVolume()
	{
		music.volume = 0.1F;
	}
}