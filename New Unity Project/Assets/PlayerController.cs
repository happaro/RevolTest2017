using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public int side = 1;

	public Animator armController, legController, spineController;
	bool isRightPunch;
	bool isRightPunchLeg;

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			isRightPunch = !isRightPunch;
			armController.SetTrigger(isRightPunch ? "punchRight" : "punchLeft");
		}

		if (Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			isRightPunchLeg = !isRightPunchLeg;
			legController.SetTrigger(isRightPunchLeg ? "punchRight" : "punchLeft");
		}

		var inputX = Input.GetAxis("Horizontal");
		transform.position += Vector3.right * inputX * speed * Time.deltaTime * side;
		//legController.SetBool("isWalking", inputX != 0);
	}
}
