using UnityEngine;

public class SaveManager
{
	public static bool IsFirstTime{get{return !PlayerPrefs.HasKey("CoinsCount");}}
	public static int CoinsCount { get { return PlayerPrefs.GetInt("CoinsCount"); } set { PlayerPrefs.SetInt("CoinsCount", value); } }

	public static bool IsHeroBought(int num){return PlayerPrefs.GetInt("IsHeroBought" + num) == 1;}
	public static void BuyHero(int num){PlayerPrefs.SetInt("IsHeroBought" + num, 1);}

	public static int GetExtraSkillLevel(int playerIndex, int skillNum){return PlayerPrefs.GetInt("ExtraSkill" + playerIndex + skillNum);}
	public static void BuyExtraSkillLevel(int playerIndex, int skillNum){PlayerPrefs.SetInt("ExtraSkill" + playerIndex + skillNum, GetExtraSkillLevel(playerIndex, skillNum) + 1);}

	public static int CurrentPlayerIndex { get { return PlayerPrefs.GetInt("CurrentPlayerIndex"); } set { PlayerPrefs.SetInt("CurrentPlayerIndex", value); } }
}
