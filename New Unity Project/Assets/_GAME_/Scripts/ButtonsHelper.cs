using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsHelper : MonoBehaviour
{
	public PlayerController player;
	public HealthBar healthLinePlayer, healthLineEnemy;
	public RectTransform joy;
	public float maxR;


	public static ButtonsHelper Instance;
	private void Awake()
	{
		Instance = this;
	}

	private Vector2 startPosition;

	private bool isHold = false;
	public void JoyDown()
	{
		Debug.LogWarning("down");
		isHold = true;
	}

	private void Update()
	{
		JoyHold();
	}

	Vector3 mousePos;
	public void JoyHold()
	{
		if (isHold)
		{
			joy.transform.position = Input.mousePosition;
			if (Mathf.Abs(joy.anchoredPosition.x) > 100 || Mathf.Abs(joy.anchoredPosition.y) > 100)
				joy.anchoredPosition = joy.anchoredPosition.normalized * 100;

			if (Mathf.Abs(joy.anchoredPosition.x) > 50)
			{
				Move((int)Mathf.Sign(joy.anchoredPosition.x));
			}
			if (Mathf.Abs(joy.anchoredPosition.y) > 30)
			{
				if (joy.anchoredPosition.y > 0)
				{
					Jump();
				}
				else
				{
					SitDown();
				}
			}
		}
	}

	public void JoyUp()
	{
		isHold = false;
		player.Stop();
		joy.anchoredPosition = Vector2.zero;
	}

	public void PunchArm()
	{
		player.PunchHand();
	}

	public void PunchLeg()
	{
		player.PunchLeg();
	}

	public void Jump()
	{
		player.Jump();
	}

	public void SitDown()
	{
		player.Sit();
	}

	public void SitUp()
	{
		player.SitUp();
	}

	public void Move(int direction)
	{
		player.Move(direction);
	}

	public void Stop()
	{
		player.Stop();
	}

}
