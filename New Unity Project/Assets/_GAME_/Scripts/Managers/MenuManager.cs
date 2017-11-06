using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	public GameBase gameBase;

	public GameObject buyButton, playButton;
	public Text playerPrice;
	public WindowDialog windowDialog;
	public WindowInfo windowInfo;
	public WindowMode windowMode;

	[Header("Player")]
	public Image playerIcon;
	public Text playerName, playerInfo, coinsCount;
	public SkillBar[] skillBars;
	public GameObject[] skillButtons;
	
	private PlayerProps currentPlayer;
	private int currentPlayerIndex;

	private int GetPlayerSkill(int i)
	{
		return currentPlayer.skills[i] + SaveManager.GetExtraSkillLevel(currentPlayerIndex, i);
	}

	private void Start()
	{
		currentPlayer = gameBase.allPlayers[currentPlayerIndex];
		UpdateHeroInfo();
		if (SaveManager.IsFirstTime)
			SaveManager.CoinsCount = 2200;
	}

	public void Play()
	{
		windowMode.Open();
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
		playerIcon.sprite = currentPlayer.avatar;
		playerName.text = currentPlayer.playerName;
		playerInfo.text = currentPlayer.playerInfo;

		for (int i = 0; i < 3; i++)
			skillBars[i].Put(GetPlayerSkill(i));
		UpdateButtons();
		UpdateCoinsCount();
	}
	public void UpgradeSkill(int skillNum)
	{
		if (SaveManager.CoinsCount >= 300)
		{
			if (currentPlayer.skills[skillNum] + SaveManager.GetExtraSkillLevel(currentPlayerIndex, skillNum) < 5)
			{
				SaveManager.BuyExtraSkillLevel(currentPlayerIndex, skillNum);
				SaveManager.CoinsCount -= 300;
				UpdateHeroInfo();
				UpdateCoinsCount();
			}
		}
		else
		{
			windowInfo.Open("Баблишка не хватает :(");
		}
	}

	void UpdateCoinsCount()
	{
		coinsCount.text = SaveManager.CoinsCount.ToString();
	}

	public void UpdateButtons()
	{
		buyButton.SetActive(!SaveManager.IsHeroBought(currentPlayerIndex));
		playButton.SetActive(SaveManager.IsHeroBought(currentPlayerIndex));
		for (int i = 0; i < 3; i++)
				skillButtons[i].SetActive(GetPlayerSkill(i) < 5 && SaveManager.IsHeroBought(currentPlayerIndex));
		playerPrice.text = currentPlayer.price.ToString();
	}

	public void BuyHero()
	{
		if (SaveManager.CoinsCount >= currentPlayer.price)
		{
			SaveManager.BuyHero(currentPlayerIndex);
			SaveManager.CoinsCount -= currentPlayer.price;
			UpdateHeroInfo();
		}
		else
		{
			windowInfo.Open("Баблишка не хватает :(");
		}
	}
}
