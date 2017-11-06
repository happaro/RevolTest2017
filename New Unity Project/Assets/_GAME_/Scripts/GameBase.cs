using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameBase : MonoBehaviour
{
	//[HideInInspector]
	public PlayerProps[] allPlayers;
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameBase))]
public class GameBaseEditor : Editor
{
	GameBase gameBase;
	public override void OnInspectorGUI()
	{
		//base.OnInspectorGUI();
		gameBase = target as GameBase;
		GUILayout.Space(10);
		GUILayout.Label("Players:");

		//if (gameBase.allPlayers == null)
		//gameBase.allPlayers = new List<PlayerProps>();
		if (gameBase.allPlayers != null && gameBase.allPlayers.Length > 0)
		{
			for (int i = 0; i < gameBase.allPlayers.Length; i++)
			{
				GUILayout.BeginHorizontal();
				gameBase.allPlayers[i].playerName = EditorGUILayout.TextField(gameBase.allPlayers[i].playerName, GUILayout.MinWidth(100));
				gameBase.allPlayers[i].playerInfo = EditorGUILayout.TextArea(gameBase.allPlayers[i].playerInfo, GUILayout.MinWidth(100));
				//EditorGUILayout.LabelField(gameBase.allPlayers[i].playerName, "", GUILayout.Width(25));
				//EditorGUILayout.IntField("Price", gameBase.allPlayers[i].price, GUILayout.Width(80));
				GUILayout.Label("Price", GUILayout.Width(30));
				gameBase.allPlayers[i].price = EditorGUILayout.IntField(gameBase.allPlayers[i].price, GUILayout.Width(40));

				GUILayout.Label("At/Sp/En", GUILayout.Width(60));
				gameBase.allPlayers[i].attack = EditorGUILayout.IntField(gameBase.allPlayers[i].attack, GUILayout.Width(20));
				gameBase.allPlayers[i].speed = EditorGUILayout.IntField(gameBase.allPlayers[i].speed, GUILayout.Width(20));
				gameBase.allPlayers[i].energy = EditorGUILayout.IntField(gameBase.allPlayers[i].energy, GUILayout.Width(20));

				//levelBase.allPlayers[i * cnt + j] = (PlayerProps)EditorGUILayout.ObjectField(levelBase.allPlayers[i * cnt + j], typeof(PlayerProps), true);
				gameBase.allPlayers[i].avatar = (Sprite)EditorGUILayout.ObjectField(gameBase.allPlayers[i].avatar, typeof(Sprite), false, GUILayout.Width(80), GUILayout.Height(80));
				//gameBase.allPlayers[i].obj = (GameObject)EditorGUILayout.ObjectField(gameBase.allPlayers[i].obj, typeof(GameObject), true, GUILayout.Width(100));
				
				GUI.backgroundColor = Color.red;
				if (GUILayout.Button("x", GUILayout.Width(30)))
					gameBase.allPlayers = gameBase.allPlayers.Where(prod => prod.playerName != gameBase.allPlayers[i].playerName).ToArray();
				GUI.backgroundColor = Color.white;
				GUILayout.EndHorizontal();

				GUILayout.Space(20);

			}
		}

		//SORTING
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Name↓", GUILayout.Height(40))) gameBase.allPlayers = gameBase.allPlayers.OrderBy(product => product.playerName).ToArray();
		if (GUILayout.Button("Price↓", GUILayout.Height(40))) gameBase.allPlayers = gameBase.allPlayers.OrderBy(product => product.price).ToArray();

		GUI.backgroundColor = Color.green;
		if (GUILayout.Button("Add item", GUILayout.Height(40)))
		{
			if (gameBase.allPlayers == null || gameBase.allPlayers.Length == 0)
			{
				gameBase.allPlayers = new PlayerProps[1];
				gameBase.allPlayers[0] = new PlayerProps();
				return;
			}
			var newArray = new PlayerProps[gameBase.allPlayers.Length + 1];
			System.Array.Copy(gameBase.allPlayers, newArray, gameBase.allPlayers.Length);
			newArray[gameBase.allPlayers.Length] = new PlayerProps();
			gameBase.allPlayers = newArray;
		}
		GUI.backgroundColor = Color.white;

		GUI.backgroundColor = Color.cyan;
		if (GUILayout.Button("Save", GUILayout.Height(40)))
		{
			UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
			UnityEditor.SceneManagement.EditorSceneManager.SaveOpenScenes();
		}
		GUI.backgroundColor = Color.white;

		EditorGUILayout.EndHorizontal();
	}
}

#endif

[Serializable]
public class PlayerProps
{
	public string playerName;
	public int attack;
	public int speed;
	public int energy;
	public int price;
	public Sprite avatar;
	public Sprite[] parts;
	public string playerInfo;
	public GameObject prefab;
	public PlayerProps()
	{
		playerName = "noName";
		price = 2000;
	}
}