using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public Image bar;
	public Image avatar;

	public void UpdateHP(float HP)
	{
		bar.fillAmount = HP / 100f;
	}
}
