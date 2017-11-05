using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
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
        if (father != null)
        {
            if (other.tag == "Enemy")
            {
                SoundManager.Instance.buffer = punchClip;
                father.CmdHit(this.transform.position, damage, father.netId.Value);
            }
        }
        else
        {
            var otherPlayerController = other.GetComponent<PlayerController>();
            if (otherPlayerController == null || playerController == otherPlayerController)
            {
                gameObject.SetActive(false);
                return;
            }
            GameObject prefab = Resources.Load<GameObject>("punchStar");
            GameObject newObj = Instantiate(prefab, this.transform.position, Quaternion.identity) as GameObject;
            newObj.transform.parent = other.transform;
            otherPlayerController.GetDamage(damage);
            SoundManager.Instance.PlayClip(punchClip, 1, Random.Range(800f, 1200f) / 1000f);
        }
        
        gameObject.SetActive(false);
    }

    
}
