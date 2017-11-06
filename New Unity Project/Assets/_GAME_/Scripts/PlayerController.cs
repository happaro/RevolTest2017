using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour
{
	public Animator armController, legController;
	private CapsuleCollider2D capsule;
	public int speed;
	public float jumpForce;
	private float healthPoints = 100;
	public bool isBot = false;
	public bool isTest = false;

	private float offsetYup = 0.1f, sizeYup = 5.7f;
	private float offsetYdown = 1.3f, sizeYdown = 3.7f;
	public SpriteRenderer[] parts;

	public PlayerController enemy;

	private PlayerStats playerStats;
	private Rigidbody2D body;
	private int currentDirection;
	private bool isRightPunch, isRightPunchLeg, died;

	public bool IsDown { get { return legController.GetBool("Down"); } }
	public GameObject ananas;

	
	private void Start()
	{
		Time.timeScale = 1;
		body = GetComponent<Rigidbody2D>();
		capsule = GetComponent<CapsuleCollider2D>();
		if (isTest && !isBot)
			ButtonsHelper.Instance.player = this;
	}

	void FuckPineapple()
	{
		ananas.SetActive(!ananas.activeSelf);
		armController.SetBool("FuckPineapple", ananas.activeSelf);
	}

	public void UpdateSettings(bool mainPlayer = false)
	{
		transform.tag = mainPlayer ? "Player" : "Enemy";
	}

	public void PushPlayerResources(PlayerProps props)
	{
		Sprite[] sprites = Resources.LoadAll<Sprite>(props.texture.name);
		int[] magicNumbers = { 3, 8, 0, 0, 1, 2, 7, 7, 5, 6 };
		for (int i = 0; i < parts.Length; i++)
			parts[i].sprite = sprites[magicNumbers[i]];
	}

	void Update()
	{
		if (died)
			return;
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
		if (Input.GetKeyUp(KeyCode.LeftShift))
			FuckPineapple();
		var inputX = Input.GetAxis("Horizontal");
		transform.position += Vector3.right * inputX * speed * Time.deltaTime;
		legController.SetBool("Walking", inputX != 0 || currentDirection != 0);
	}

    private void MakeAction()
    {
        float distance = transform.position.x - enemy.transform.position.x;
        var changeDirectionPosibility = currentDirection == 1 ? 300 : 20;
        if (Random.Range(0, changeDirectionPosibility) == 0)
            currentDirection = Random.Range(-1, 2);
        if (Mathf.Abs(distance)>1.8)
        {
            int sidee = distance > 0 ? -1 : 1;
            transform.position += Vector3.right * sidee * speed * Time.deltaTime * currentDirection;
        }
        else 
            currentDirection = 0;
        if (currentDirection != 0)
            legController.SetBool("Walking", true);
        else
            legController.SetBool("Walking", false);

        if (Random.Range(0, 500) == 0) Jump();
        if (Mathf.Abs(distance)<3) 
        {
            switch (Random.Range(0, 30))                
            {
                case 0: PunchHand(); break;
                case 1: PunchLeg(); break;
            }
        }
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
			ButtonsHelper.Instance.healthLinePlayer.UpdateHP(healthPoints);
		else ButtonsHelper.Instance.healthLineEnemy.UpdateHP(healthPoints);
	}

	public void Die()
	{
		if (!died)
		{
			FindObjectOfType<FightManager>().dialog.Open("Вы проебали. Играть еще раз?", () => 
			{
				SceneController.Instance.gameMode = SceneController.GameMode.Offline;
				SceneController.Instance.LoadScene("FightScene");
			}, () => { SceneController.Instance.LoadScene("Menu"); });
			died = true;
			Time.timeScale = 0;
			body.bodyType = RigidbodyType2D.Kinematic;
			capsule.size = new Vector2(2, 2);
		}
	}

	void SetCapsuleSize(bool big)
	{
		if (big)
		{
			capsule.size = new Vector2(capsule.size.x, sizeYup);
			capsule.offset = new Vector2(capsule.offset.x, offsetYup);
		}
		else
		{
			capsule.size = new Vector2(capsule.size.x, sizeYdown);
			capsule.offset = new Vector2(capsule.offset.x, offsetYdown);
		}
	}

	public void Move(int direction)
	{
		currentDirection = direction;
	}

	public void Stop()
	{
		currentDirection = 0;
	}

	public void PunchHand()
	{
		//SoundManager.Instance.PlayClip(handWhip);
		isRightPunch = !isRightPunch;
		armController.SetTrigger(isRightPunch ? "PunchRight" : "PunchLeft");
	}

	public void PunchLeg()
	{
		//SoundManager.Instance.PlayClip(legWhip);
		isRightPunchLeg = !isRightPunchLeg;
		legController.SetTrigger(isRightPunchLeg ? "PunchRight" : "PunchLeft");
		legController.SetBool("Jump", false);

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.tag == "Floor")
		{
			legController.SetBool("Jump", false);
			//SetCapsuleSize(true);
		}
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
		legController.SetBool("Down", true);
		SetCapsuleSize(false);
	}

	public void SitUp()
	{
		legController.SetBool("Down", false);
		SetCapsuleSize(true);
	}
}
