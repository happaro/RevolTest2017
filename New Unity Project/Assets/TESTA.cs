using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTA : MonoBehaviour {

	public GameObject IK;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		IK.transform.position += Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal");	
	}
}
