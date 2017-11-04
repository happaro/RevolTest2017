using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
	public RectTransform bar;

	public float maxWidth;


	void Start ()
	{
		
	}

	public void UpdateHP(float HP)
	{
		bar.sizeDelta = new Vector2((HP / 100f) * maxWidth, bar.sizeDelta.y);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
