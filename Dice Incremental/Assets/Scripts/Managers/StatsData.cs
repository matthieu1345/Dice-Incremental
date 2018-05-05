using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatsData
{
	[SerializeField]
	private int m_totalGold;

	public void AddGold( int value ) { m_totalGold += value; }

	[SerializeField]
	private int m_totalXp;

	public void AddXp( int value ) { m_totalXp += value; }

	[SerializeField]
	private int m_totalRolls;

	public void AddRolls( int value ) { m_totalRolls += value; }

	[SerializeField]
	private int m_totalEyes;

	public void AddEyes( int value ) { m_totalEyes += value; }

}
