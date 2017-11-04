using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
	public PlayerController father;
	public float damage;

	private void Start()
	{
		father = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy")
		{
			other.GetComponent<PlayerController>().GetDamage(damage);
			gameObject.SetActive(false);
		}
	}
}
