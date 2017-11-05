using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    public void RunBotFightScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BotFight");
        Debug.Log("RunBotFightScene");
    }
    public void RunNetworkFightScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
        Debug.Log("RunNetworkFightScene");
    }
}
