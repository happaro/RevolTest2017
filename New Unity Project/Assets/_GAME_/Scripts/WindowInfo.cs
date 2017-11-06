using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowInfo : MonoBehaviour
{
	public UnityEngine.UI.Text txt;
	public void Open(string text)
	{
		gameObject.SetActive(true);

		txt.text = text;
	}

	public void Ok()
	{
		gameObject.SetActive(false);
	}
}
