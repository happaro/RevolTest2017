using System;
using UnityEngine;

public class WindowDialog : MonoBehaviour
{
	public UnityEngine.UI.Text txt;
	Action yesAction;
	public void Open(string text, Action yes)
	{
		gameObject.SetActive(true);
		this.yesAction = yes;
		txt.text = text;
	}

	public void YesAction()
	{
		yesAction();
	}

	public void Ok()
	{
		gameObject.SetActive(false);
	}
}
