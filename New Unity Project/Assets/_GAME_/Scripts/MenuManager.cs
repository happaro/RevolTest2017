using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	public GameBase gameBase;

	public GameObject buyButton, playButton;
	public Text playerPrice;

	[Header("Player")]
	public Image playerIcon;
	public Text playerName, playerInfo, coinsCount;
	public SkillBar[] skillBars;
	
	private PlayerProps currentPlayer;
	private int currentPlayerIndex;

	public void Play()
	{
		SceneController.Instance.LoadScene("Mode");
	}

	public void ChangeHero(int dir)
	{
		currentPlayerIndex += dir;
		currentPlayerIndex = currentPlayerIndex >= gameBase.allPlayers.Length ? 0 : currentPlayerIndex == -1 ? gameBase.allPlayers.Length - 1 : currentPlayerIndex;
		currentPlayer = gameBase.allPlayers[currentPlayerIndex];
		UpdateHeroInfo();
	}

	public void UpdateHeroInfo()
	{
		//TODO: BUY IF NOT KUPLENO
		playerIcon.sprite = currentPlayer.avatar;
		playerName.text = currentPlayer.playerName;
		playerInfo.text = currentPlayer.playerInfo;

		////TODO: ADD SKILLS
		skillBars[0].Put(currentPlayer.attack);
		skillBars[1].Put(currentPlayer.speed);
		skillBars[2].Put(currentPlayer.energy);
	}

	public void UpdateButtons()
	{
		//
	}

	public void OpenShop()
	{

	}

	public void OpenUpgrade()
	{

	}
}
