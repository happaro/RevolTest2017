using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
	public PlayerSetup father;
	public float damage;
	public AudioClip punchClip;

	private void Start()
	{
		father = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSetup>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy")
		{
			father.CmdHit(this.transform.position, damage, father.netId.Value);
			//GameObject prefab = Resources.Load<GameObject>("punchStar");
			//GameObject newObj = Instantiate(prefab, this.transform.position, Quaternion.identity) as GameObject;
			//newObj.transform.parent = other.transform;
			//SoundManager.Instance.PlayClip(punchClip, 1, Random.Range(800f, 1200f) / 1000f);
			//other.GetComponent<PlayerController>().GetDamage(damage);
			gameObject.SetActive(false);
		}
	}
}
