using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Animator armController, legController;
	public CapsuleCollider2D capsule;
	public int speed;
	public float jumpForce;
	public float healthPoints = 100;
	public bool isBot = false;

	public PlayerController enemy;
	public PlayerStats playerStats;

	private Rigidbody2D body;
	private int currentDirection;
	private float offsetYup = -0.7f, offsetYdown = 0.1f, sizeYup = 7.9f, sizeYdown = 6.5f;
	private bool isRightPunch, isRightPunchLeg, died;

	private void Start()
	{
		body = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (enemy != null)
		{
			int side = transform.position.x - enemy.transform.position.x > 0 ? -1 : 1;
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * side, transform.localScale.y, transform.localScale.z);
		}

        if(isBot)
        {
            MakeAction();
            return;
        }

		if (tag == "Enemy")
			return;

        KeyboardCheck();

		transform.position += Vector3.right * currentDirection * speed * Time.deltaTime;
	}

	private void KeyboardCheck()
	{
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
		transform.position += Vector3.right * inputX * speed * Time.deltaTime;
		legController.SetBool("isWalking", inputX != 0 || currentDirection != 0);
	}

    private void MakeAction()
    {
        float distance = transform.position.x - enemy.transform.position.x;
        Debug.Log("distance = " + distance);
        if (Mathf.Abs(distance)>2)
        {
            int sidee = distance > 0 ? -1 : 1;
            transform.position += Vector3.right * sidee * speed * Time.deltaTime;
            legController.SetBool("isWalking", true);
        }
        switch (Random.Range(0, 500))
        {
            case 0: Jump(); break;
        }
        if (Mathf.Abs(distance)<3) 
        {
            switch (Random.Range(0, 50))                
            {
                case 0: PunchHand(); break;
                case 1: PunchLeg(); break;
            }
        }
        legController.SetBool("isWalking", false);

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
		if (tag == "Player")
			ButtonsHelper.Instace.healthLinePlayer.UpdateHP(healthPoints);
		else ButtonsHelper.Instace.healthLineEnemy.UpdateHP(healthPoints);
	}

	public void Die()
	{
		if (!died)
		{
			died = true;
			this.transform.Rotate(0, 0, -90);
			//if (deadClip != null)
			//SoundManager.Instance.PlayClip(deadClip);
			capsule.size = new Vector2(2, 2);
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
			currentDirection = 0;
	}

	public void PunchHand()
	{
		//SoundManager.Instance.PlayClip(handWhip);
		isRightPunch = !isRightPunch;
		armController.SetTrigger(isRightPunch ? "punchRight" : "punchLeft");
	}

	public void PunchLeg()
	{
		//SoundManager.Instance.PlayClip(legWhip);
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
