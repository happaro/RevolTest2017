﻿using UnityEngine;
using UnityEngine.Networking;

public class FightManager : MonoBehaviour
{
	public GameObject playerPrefab;
	public GameBase gameBase;
	public WindowDialog dialog;
	public WindowInfo info;

	void Start()
	{
		if (SceneController.Instance != null)
		{
			if (SceneController.Instance.gameMode == SceneController.GameMode.Offline)
			{
				var player = (Instantiate(playerPrefab) as GameObject).GetComponent<PlayerController>();
				var bot = (Instantiate(playerPrefab) as GameObject).GetComponent<PlayerController>();

				player.enemy = bot;
				player.transform.position = new Vector3(-6, -2, 0);
				bot.enemy = player;
				bot.isBot = true;
				bot.tag = "Enemy";
				bot.transform.position = new Vector3(6, -2, 0);

				var playerProps = gameBase.allPlayers[SaveManager.CurrentPlayerIndex];

				ButtonsHelper.Instance.player = player;
				ButtonsHelper.Instance.healthLinePlayer.avatar.sprite = playerProps.avatar;
				player.PushPlayerResources(playerProps);

				//INIT BOT
                bot.PushPlayerResources(gameBase.allPlayers[Random.Range(0, 4)]);
			}
			else
			{
				
			}
		}
	}
}
