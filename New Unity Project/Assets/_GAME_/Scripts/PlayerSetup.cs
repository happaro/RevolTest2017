using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    public Behaviour[] componentsToDisable;
	public PlayerController myPlayer, enemyPlayer;
	private PlayerSetup[] objs;

	void Start()
    {
		objs = FindObjectsOfType<PlayerSetup>();
        if (objs.Length == 1)
        {
            if (objs[0].isLocalPlayer)
            {
                objs[0].GetComponent<PlayerController>().tag = "Player";
                ButtonsHelper.Instance.player = objs[0].GetComponent<PlayerController>();
            }
        }
		else if (objs.Length > 1)
		{
			if (objs[0].isLocalPlayer)
			{
				objs[0].GetComponent<PlayerController>().tag = "Player";
				objs[0].GetComponent<PlayerController>().enemy = objs[1].GetComponent<PlayerController>();
				objs[1].GetComponent<PlayerController>().tag = "Enemy";
				objs[1].GetComponent<PlayerController>().enemy = objs[0].GetComponent<PlayerController>();
				ButtonsHelper.Instance.player = objs[0].GetComponent<PlayerController>();
			}
			if (objs[1].isLocalPlayer)
			{
				objs[1].GetComponent<PlayerController>().tag = "Player";
				objs[1].GetComponent<PlayerController>().enemy = objs[0].GetComponent<PlayerController>();
				objs[0].GetComponent<PlayerController>().tag = "Enemy";
				objs[0].GetComponent<PlayerController>().enemy = objs[1].GetComponent<PlayerController>();
				ButtonsHelper.Instance.player = objs[1].GetComponent<PlayerController>();
			}
		}
        if (!isLocalPlayer)
        {
			for (int i = 0; i < componentsToDisable.Length; i++)
                componentsToDisable[i].enabled = false;
            DisableAllAnimators(transform);
        }
    }

	[Command]
	public void CmdHit(Vector3 position, float damage, uint id)
	{
		RpcHit(position, damage, id);
	}

	[ClientRpc]
	private void RpcHit(Vector3 position, float damage, uint id)
	{
		objs = FindObjectsOfType<PlayerSetup>();
		var enemy = objs[0].netId.Value != id ? objs[0] : objs[1];
		GameObject prefab = Resources.Load<GameObject>("punchStar");
		GameObject newObj = Instantiate(prefab, position + Vector3.back, Quaternion.identity) as GameObject;
		//newObj.transform.parent = enemy.transform;
		enemy.GetComponent<PlayerController>().GetDamage(damage);
        if(SoundManager.Instance.buffer!= null)
        {
            SoundManager.Instance.PlayClip(SoundManager.Instance.buffer, 1, Random.Range(800f, 1200f) / 1000f);
        }
        
		//GameObject.FindGameObjectWithTag("Console").GetComponent<UnityEngine.UI.Text>().text += "\n" + id.ToString();
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
