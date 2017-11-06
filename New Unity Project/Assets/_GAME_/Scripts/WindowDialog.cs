using System;
using UnityEngine;

public class WindowDialog : MonoBehaviour
{
	public UnityEngine.UI.Text txt;
	Action yesAction;
	Action noAction;
	public void Open(string text, Action yes, Action no = null)
	{
		gameObject.SetActive(true);
		yesAction = yes;
		txt.text = text;
		noAction = no;
	}

	public void YesAction()
	{
		yesAction();
		Ok();
	}

	public void NoAction()
	{
		noAction();
	}

	public void Ok()
	{
		gameObject.SetActive(false);
	}
}
