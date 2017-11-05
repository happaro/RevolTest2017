using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	public PlayerStat[] stats;
}

public class PlayerStat
{
	public PlayerStat(int power, int speed, int energy)
	{
		this.power = power;
		this.speed = speed;
		this.energy = energy;
	}
	public int health;

	public int power;
	public int energy;
	public int speed;
}
