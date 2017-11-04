using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsHelper : MonoBehaviour
{
	public PlayerController player;

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

	public void Move(int direction)
	{
		player.Move(direction);
	}

	public void Stop(int direction)
	{
		player.Stop(direction);
	}
}
