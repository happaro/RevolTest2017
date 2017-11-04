using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class MyNetworkManager : NetworkManager
{
	public bool iamHost;
	public UnityEngine.UI.Text console;

	//FOR CLIENT
	public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		base.OnMatchJoined(success, extendedInfo, matchInfo);
		Invoke("InitPlayersInfo", 0.5F);
	}

	//FOR HOST
	public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		base.OnMatchCreate(success, extendedInfo, matchInfo);
		iamHost = true;
	}

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		base.OnServerAddPlayer(conn, playerControllerId);
		if (conn.hostId == 0)
			Invoke("InitPlayersInfo", 0.5F);
	}
}
