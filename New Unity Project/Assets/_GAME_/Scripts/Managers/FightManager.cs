using UnityEngine;

public class FightManager : MonoBehaviour
{
	public GameObject offlinePrefab, botPrefab;
	public GameBase gameBase;

	void Start()
	{
		if (SceneController.Instance.gameMode == SceneController.GameMode.Offline)
		{
			var player = (Instantiate(offlinePrefab) as GameObject).GetComponent<PlayerController>();
			var bot = (Instantiate(botPrefab) as GameObject).GetComponent<PlayerController>();
			player.enemy = bot;
			bot.enemy = player;
			var playerProps = gameBase.allPlayers[SaveManager.CurrentPlayerIndex];

			ButtonsHelper.Instance.player = player;
			ButtonsHelper.Instance.healthLinePlayer.avatar.sprite = playerProps.avatar;
			player.PushPlayerResources(playerProps);
			//INIT BOT
		}
		else
		{
			//SET ANOTHER PLAYER
		}
	}
}
