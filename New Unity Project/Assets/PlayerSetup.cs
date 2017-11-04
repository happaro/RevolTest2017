using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    public Behaviour[] componentsToDisable;
	public PlayerController myPlayer, enemyPlayer;
    void Start()
    {
		var objs = FindObjectsOfType<PlayerSetup>();
		if (objs.Length > 1)
		{
			if (objs[0].isLocalPlayer)
			{
				objs[0].GetComponent<PlayerController>().tag = "Player";
				objs[0].GetComponent<PlayerController>().enemy = objs[1].GetComponent<PlayerController>();
				objs[1].GetComponent<PlayerController>().tag = "Enemy";
			}
			if (objs[1].isLocalPlayer)
			{
				objs[1].GetComponent<PlayerController>().tag = "Player";
				objs[1].GetComponent<PlayerController>().enemy = objs[0].GetComponent<PlayerController>();
				objs[0].GetComponent<PlayerController>().tag = "Enemy";
			}
		}
        if (!isLocalPlayer)
        {
			for (int i = 0; i < componentsToDisable.Length; i++)
                componentsToDisable[i].enabled = false;
            DisableAllAnimators(transform);
        }
    }

    private void DisableAllAnimators(Transform t)
    {
        foreach (var animator in  t.gameObject.GetComponents<Animator>())
        {
            animator.enabled = false;
        }
        foreach (object obj in t)
        {
            Transform child = (Transform)obj;
            DisableAllAnimators(child);
        }
    }
}
