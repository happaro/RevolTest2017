using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowInfo : MonoBehaviour
{
	public UnityEngine.UI.Text txt;
	System.Action okAct;

	public void Open(string text, System.Action okAction = null)
	{
		gameObject.SetActive(true);
		okAct = okAction;
		txt.text = text;
	}

	public void Ok()
	{
		gameObject.SetActive(false);
		if (okAct != null)
		{
			okAct();
		}
	}
}
