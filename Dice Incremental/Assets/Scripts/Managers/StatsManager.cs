using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class StatsManager : InstancedMonoBehaviour<StatsManager>
{
	[SerializeField]
	private StatsData m_stats;

	public void LoadStats(StatsData stats) { m_stats = stats; }

	public StatsData GetStats() { return m_stats; }

	public void RecievedMoney(int moneyValue)
	{
		m_stats.AddGold(moneyValue);
	}

	public void RecievedXp( int xpValue )
	{
		m_stats.AddXp(xpValue);
	}

	public void RolledDice(int eyeValue)
	{
		m_stats.AddRolls(1);
		m_stats.AddEyes(eyeValue);
	}

	public void TakenRoll()
	{
		m_stats.AddTurn(1);
	}

}
