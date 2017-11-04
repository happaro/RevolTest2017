using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchStar : MonoBehaviour
{
	private float a = 1;
	private SpriteRenderer myRend;

	void Start ()
	{
		myRend = this.GetComponent<SpriteRenderer>();
	}
	
	void Update ()
	{
		myRend.color = new Color(1, 1, 1, a);
		a -= Time.deltaTime * 2;
	}
}
