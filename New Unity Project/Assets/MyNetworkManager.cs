using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class MyNetworkManager : NetworkManager
{
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		base.OnServerAddPlayer(conn, playerControllerId);
		FindObjectOfType<ButtonsHelper>().player = FindObjectOfType<PlayerController>();
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.O))
			Debug.LogWarning(GameObject.FindObjectOfType<PlayerController>() != null);
	}
}
