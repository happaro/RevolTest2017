using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoPrefab : MonoBehaviour
{
	public Text playerName, info;
	public SkillBar bar1, bar2, bar3;
	public Image avatar;

	public void Init(PlayerProps props)
	{
		playerName.text = props.playerName;
		info.text = props.playerInfo;
		bar1.Put(props.attack);
		bar2.Put(props.speed);
		bar3.Put(props.energy);
	}
}