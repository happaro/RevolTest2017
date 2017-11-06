using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBar : MonoBehaviour
{
	public void Put(int value)
	{
		for (int i = 1; i <= 5; i++)
		{
			transform.GetChild(i).gameObject.SetActive(value >= i);
		}
	}
}
