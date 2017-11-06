using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour {

	[SerializeField]
	private uint roomSize = 6;
	public UnityEngine.UI.InputField roomNameText;
	public string[] randomWords;
	public string[] randomWords1;
	private string roomName;

	private NetworkManager networkManager;

	void Start ()
	{
		string str = randomWords[Random.Range(0, randomWords.Length)] + randomWords1[Random.Range(0, randomWords1.Length)];
		roomNameText.text = str;
		networkManager = NetworkManager.singleton;
		if (networkManager.matchMaker == null)
			networkManager.StartMatchMaker();
	}

	public void SetRoomName (string _name)
	{
		roomName = _name;
	}

	public void CreateRoom ()
	{
		if (roomName != "" && roomName != null)
		{
			Debug.Log("Creating Room: " + roomName + " with room for " + roomSize + " players.");
			networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
		}
	}
}
