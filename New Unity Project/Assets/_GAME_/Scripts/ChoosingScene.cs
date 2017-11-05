using UnityEngine;

public class ChoosingScene : MonoBehaviour
{
	public void LoadBotFight()
	{
		SceneController.Instance.gameMode = SceneController.GameMode.Offline;
		SceneController.Instance.LoadScene("FightScene");
	}

	public void LoadOnlineFight()
	{
		SceneController.Instance.gameMode = SceneController.GameMode.Offline;
		SceneController.Instance.LoadScene("FightScene");
	}
}