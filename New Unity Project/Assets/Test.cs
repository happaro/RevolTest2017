using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
	public float timer;
	public Text text;
	public GameObject IK;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.P))
			Time.timeScale = 0;
		timer += Time.deltaTime;
		//Debug.LogWarning(timer);
		text.text = (int)timer + "\n" + IK.transform.position.ToString();
	}
}
