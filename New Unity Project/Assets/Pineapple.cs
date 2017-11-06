using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pineapple : MonoBehaviour
{
	Rigidbody2D rigid;
	public int direction;
	[HideInInspector]
	public PlayerSetup father;
	public PlayerController playerController;
	public int damage;
	public AudioClip punchClip;
	void Start ()
	{
		rigid = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
	{
		if (rigid.velocity.x < 6)
			GetComponent<Rigidbody2D>().AddForce(new Vector2(20 * direction, 0));
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (father != null && SceneController.Instance.gameMode == SceneController.GameMode.Online)
		{
			if (other.transform.tag == "Enemy")
			{
				SoundManager.Instance.buffer = punchClip;
				father.CmdHit(this.transform.position, damage, father.netId.Value);
				gameObject.SetActive(false);
			}
		}
		else
		{
			var otherPlayerController = other.transform.GetComponent<PlayerController>();
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
				: Vector2.right) * damage * 25);
			SoundManager.Instance.PlayClip(punchClip, 1, Random.Range(800f, 1200f) / 1000f);
			gameObject.SetActive(false);
		}
	}
}
