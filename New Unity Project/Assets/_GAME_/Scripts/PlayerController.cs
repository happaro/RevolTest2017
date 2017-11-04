using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public float walkSpeed;
	public float jumpForce;
	public int side = 1;
	public int speed;
	public Rigidbody2D body;
	public float healthPoints = 100;
	public HealthBar healthLine;
	//public Image energyLine;
	public AudioClip handPunchSound, legPunchSound;
	public AudioClip handWhip, legWhip;
	public CapsuleCollider2D capsule;
	public AudioClip deadClip;

	[HideInInspector]
	public PlayerController enemy;

	public PlayerStats playerStats;
	private bool isMainPlayer;
	public bool isEnemy = false;

	public Animator armController, legController, spineController;
	bool isRightPunch;
	bool isRightPunchLeg;

	private int currentDirection;
	public HitPoint[] handsPoints;
	public HitPoint[] lagsPoints;

	float offsetYup = -0.7f;
	float offsetYdown = 0.1f;
	float sizeYup = 7.9f;
	float sizeYdown = 6.5f;


	public void Init()
	{
		//if (tag == "Enemy")
	}

	void Update()
	{
		int sidee = transform.position.x - enemy.transform.position.x > 0 ? -1 : 1;
		transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * sidee, transform.localScale.y, transform.localScale.z);

		if (tag == "Enemy")
			return;
		transform.position += Vector3.right * currentDirection * speed * Time.deltaTime * side;
		if (Input.GetKeyDown(KeyCode.F))
			PunchHand();
		if (Input.GetKeyDown(KeyCode.G))
			PunchLeg();
		if (Input.GetKeyDown(KeyCode.Space))
			Jump();
		if (Input.GetKeyDown(KeyCode.DownArrow))
			Sit();
		if (Input.GetKeyUp(KeyCode.DownArrow))
			SitUp();

		var inputX = Input.GetAxis("Horizontal");
		transform.position += Vector3.right * inputX * speed * Time.deltaTime * side;
		legController.SetBool("isWalking", inputX != 0 || currentDirection != 0);
	}

	public void GetDamage(float damage)
	{
		healthPoints -= damage;
		if (healthPoints <= 0)
			Die();
		UpdateHP();
	}

	void UpdateHP()
	{
		//healthLine.UpdateHP(healthPoints);
	}

	bool died = false;

	public void Die()
	{
		if (!died)
		{
			died = true;
			this.transform.Rotate(0, 0, -90);
			if (deadClip != null)
				SoundManager.Instance.PlayClip(deadClip);
			capsule.size = new Vector2(2,2);
		}
		
	}

	

	public void Move(int direction)
	{
		int sidee = transform.position.x - enemy.transform.position.x > 0 ? 1 : -1;
		transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * sidee, transform.localScale.y, transform.localScale.z);
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
		SoundManager.Instance.PlayClip(handWhip);
		isRightPunch = !isRightPunch;
		armController.SetTrigger(isRightPunch ? "punchRight" : "punchLeft");
	}

	public void PunchLeg()
	{
		SoundManager.Instance.PlayClip(legWhip);
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

	public void Sit()
	{
		legController.SetBool("isDown", true);
		capsule.size = new Vector2(capsule.size.x, sizeYdown);
		capsule.offset = new Vector2(capsule.offset.x, offsetYdown);
	}

	public void SitUp()
	{
		legController.SetBool("isDown", false);
		capsule.size = new Vector2(capsule.size.x, sizeYup);
		capsule.offset = new Vector2(capsule.offset.x, offsetYup);
	}
}
