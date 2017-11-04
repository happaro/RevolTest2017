using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float jumpForce;
	public int side = 1;

	public Animator armController, legController, spineController;
	bool isRightPunch;
	bool isRightPunchLeg;
	public Rigidbody2D body;

	private int currentDirection;


	void Update ()
	{

		transform.position += Vector3.right * currentDirection * speed * Time.deltaTime * side;

		//legController.SetBool("isWalking", currentDirection != 0);
		if (Input.GetKeyDown(KeyCode.F))
		{
			isRightPunch = !isRightPunch;
			armController.SetTrigger(isRightPunch ? "punchRight" : "punchLeft");
		}

		if (Input.GetKeyDown(KeyCode.G))
		{
			isRightPunchLeg = !isRightPunchLeg;
			legController.SetTrigger(isRightPunchLeg ? "punchRight" : "punchLeft");
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			body.AddForce(Vector2.up * jumpForce);
		}
		var inputX = Input.GetAxis("Horizontal");
		transform.position += Vector3.right * inputX * speed * Time.deltaTime * side;
		legController.SetBool("isWalking", inputX != 0 || currentDirection != 0);
	}

	public void Move(int direction)
	{
		currentDirection = direction;
	}

	public void Stop(int direction)
	{
		if (direction == currentDirection)
		{
			currentDirection = 0;
		}
	}

	public void PunchHand()
	{
		isRightPunch = !isRightPunch;
		armController.SetTrigger(isRightPunch ? "punchRight" : "punchLeft");
	}

	public void PunchLeg()
	{
		isRightPunchLeg = !isRightPunchLeg;
		legController.SetTrigger(isRightPunchLeg ? "punchRight" : "punchLeft");
	}
}
