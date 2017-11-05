using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsHelper : MonoBehaviour
{
	public HitPoint[] points;
	public bool isActiveLeft;
	public bool isActiveRight;


	public void ActivePoint(int num)
	{
		points[num].gameObject.SetActive(true);
	}

	public void DisActivePoint(int num)
	{
		points[num].gameObject.SetActive(false);
	}
}
