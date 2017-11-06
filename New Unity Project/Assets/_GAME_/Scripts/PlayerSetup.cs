using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    public GameBase gameBase;
    public Behaviour[] componentsToDisable;
    public PlayerController myPlayer, enemyPlayer;
    private PlayerSetup[] objs;

    void Start()
    {
        if (SceneController.Instance.gameMode == SceneController.GameMode.Online)
        {
            if (!isLocalPlayer)
            {
                for (int i = 0; i < componentsToDisable.Length; i++)
                    componentsToDisable[i].enabled = false;
                DisableAllAnimators(transform);
            }
        }
        objs = FindObjectsOfType<PlayerSetup>();
        if (objs.Length == 1)
        {
            if (objs[0].isLocalPlayer)
            {
                myPlayer = objs[0].GetComponent<PlayerController>();
                myPlayer.tag = "Player";
                ButtonsHelper.Instance.player = myPlayer;
                objs = FindObjectsOfType<PlayerSetup>();
                var playerProps = gameBase.allPlayers[SaveManager.CurrentPlayerIndex];
                ButtonsHelper.Instance.healthLinePlayer.avatar.sprite = playerProps.avatar;
                myPlayer.PushPlayerResources(playerProps);
            }
        }
        else if (objs.Length > 1)
        {
            if (objs[0].isLocalPlayer)
            {
                myPlayer = objs[0].GetComponent<PlayerController>();
                enemyPlayer = objs[1].GetComponent<PlayerController>();
            }
            else
            {
                myPlayer = objs[1].GetComponent<PlayerController>();
                enemyPlayer = objs[0].GetComponent<PlayerController>();
            }
            myPlayer.tag = "Player";
            myPlayer.enemy = enemyPlayer;
            enemyPlayer.tag = "Enemy";
            enemyPlayer.enemy = myPlayer;
            CmdLoadSkin(SaveManager.CurrentPlayerIndex, base.netId.Value);
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
        Instantiate(prefab, position + Vector3.back, Quaternion.identity);
        //newObj.transform.parent = enemy.transform;
        enemy.GetComponent<PlayerController>().GetDamage(damage);
        enemy.GetComponent<PlayerController>().body
                .AddForce((enemy.transform.position.x - (objs[0].netId.Value != id ? objs[1] : objs[0]).transform.position.x < 0
                ? Vector2.left
                : Vector2.right) * damage * 25);
        if (SoundManager.Instance.buffer != null)

        {
            SoundManager.Instance.PlayClip(SoundManager.Instance.buffer, 1, Random.Range(800f, 1200f) / 1000f);
        }

        //GameObject.FindGameObjectWithTag("Console").GetComponent<UnityEngine.UI.Text>().text += "\n" + id.ToString();
    }

    [Command]
    public void CmdLoadSkin(int index, uint id)
    {
        RpcLoadSkin(index, id);
    }

    [ClientRpc]
    private void RpcLoadSkin(int index, uint id)
    {
        objs = FindObjectsOfType<PlayerSetup>();
        var player = objs[0].netId.Value == id ? objs[0] : objs[1];
        var playerControler = player.gameObject.GetComponent<PlayerController>();
        var playerProps = gameBase.allPlayers[index];
        ButtonsHelper.Instance.healthLinePlayer.avatar.sprite = playerProps.avatar;
        playerControler.PushPlayerResources(playerProps);

    }

    private void DisableAllAnimators(Transform t)
    {
        foreach (var animator in t.gameObject.GetComponents<Animator>())
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
