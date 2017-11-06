using System;
using UnityEngine;

public class WindowMode : MonoBehaviour
{
	public void Open()
	{
		gameObject.SetActive(true);
	}

	public void LoadOffline()
	{
		SceneController.Instance.gameMode = SceneController.GameMode.Offline;
		SceneController.Instance.LoadScene("FightScene");
	}

	public void LoadOnline()
	{
		SceneController.Instance.gameMode = SceneController.GameMode.Online;
		SceneController.Instance.LoadScene("Lobby");
	}

	public void Close()
	{
		gameObject.SetActive(false);
	}
}
