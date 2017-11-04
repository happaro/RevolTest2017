using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public float walkSpeed;
	public float jumpForce;
	public int side = 1;
	public Rigidbody2D body;

	public PlayerStats playerStats;
	private bool isMainPlayer;

	public Animator armController, legController, spineController;
	bool isRightPunch;
	bool isRightPunchLeg;

	private int currentDirection;

	private PlayerStat myStats { get { return playerStats.stats[isMainPlayer ? 0 : 1]; } set
		{
			playerStats.stats[isMainPlayer ? 0 : 1] = value;
		} }
	void Update()
	{
		transform.position += Vector3.right * currentDirection * myStats.speed * Time.deltaTime * side;
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
			Jump();

		}
		var inputX = Input.GetAxis("Horizontal");
		transform.position += Vector3.right * inputX * myStats.speed * Time.deltaTime * side;
		legController.SetBool("isWalking", inputX != 0 || currentDirection != 0);
	}

	public void GetDamage()
	{

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


	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.tag == "Floor")
			legController.SetBool("Jump", false);
	}
	public void Jump()
	{
		//TODO: IF NOT IN AIR..
		if (!legController.GetBool("Jump"))
		{
			body.AddForce(Vector2.up * jumpForce);
			legController.SetBool("Jump", true);
		}
	}
}
