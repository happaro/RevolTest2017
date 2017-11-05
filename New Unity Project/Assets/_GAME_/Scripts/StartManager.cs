using UnityEngine;

public class StartManager : MonoBehaviour
{
	public GameObject offlinePrefab, botPrefab;
	void Start()
	{
		if (SceneController.Instance.gameMode == SceneController.GameMode.Offline)
		{
			var player = (Instantiate(offlinePrefab) as GameObject).GetComponent<PlayerController>();
			var bot = (Instantiate(botPrefab) as GameObject).GetComponent<PlayerController>();
			player.enemy = bot;
			bot.enemy = player;
			ButtonsHelper.Instance.player = player;
		}
	}
}
