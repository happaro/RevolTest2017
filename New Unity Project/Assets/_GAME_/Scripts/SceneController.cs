using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
	public enum GameMode { Offline, Online}
	public GameMode gameMode;

	public static SceneController Instance;
	private void Awake()
	{
		if (!Instance)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
			Destroy(gameObject);
	}

	public void LoadScene(string sceneName)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
	}
}
