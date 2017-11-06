using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
	[HideInInspector]
	public PlayerSetup father;
    public PlayerController playerController;
	public float damage;
	public AudioClip punchClip;
	private void Start()
	{
		father = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSetup>();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (father != null && SceneController.Instance.gameMode == SceneController.GameMode.Online)
        {
            if (other.tag == "Enemy" )
            {
                SoundManager.Instance.buffer = punchClip;
                father.CmdHit(this.transform.position, damage, father.netId.Value);
				gameObject.SetActive(false);
			}
		}
        else
        {
            var otherPlayerController = other.GetComponent<PlayerController>();
            if (otherPlayerController == null || playerController == otherPlayerController)
            {
                return;
            }
            GameObject prefab = Resources.Load<GameObject>("punchStar");
            GameObject newObj = Instantiate(prefab, this.transform.position, Quaternion.identity) as GameObject;
            newObj.transform.parent = other.transform;
            otherPlayerController.GetDamage(damage);
            otherPlayerController.body
                .AddForce((otherPlayerController.transform.position.x - playerController.transform.position.x < 0 
                ? Vector2.left 
                : Vector2.right) * damage*25);
            SoundManager.Instance.PlayClip(punchClip, 1, Random.Range(800f, 1200f) / 1000f);
            gameObject.SetActive(false);
        }   
    }
}
