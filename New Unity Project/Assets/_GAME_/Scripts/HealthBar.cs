using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public Image bar;
	public Image avatar;
	public float maxWidth;

	public void UpdateHP(float HP)
	{
		bar.fillAmount = HP / 100f;
	}
}
