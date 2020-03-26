using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatsData
{
	[SerializeField]
	public int m_totalGold {get; private set; }
	[SerializeField]
	public int m_highestPrestigeGold {get; private set; }
	public int c_currentPrestigeGold {get; private set; }
	[SerializeField]
	public int m_highestTurnGold {get; private set; }
	public int c_currentTurnGold {get; private set; }
	public void AddGold( int value ) 
	{
		m_totalGold += value;

		c_currentPrestigeGold += value;
		if (m_highestPrestigeGold < c_currentPrestigeGold)
			m_highestPrestigeGold = c_currentPrestigeGold;

		c_currentTurnGold += value;
		if (m_highestTurnGold < c_currentTurnGold)
			m_highestTurnGold = c_currentTurnGold;
	}

	[SerializeField]
	public int m_totalXp {get; private set; }
	public void AddXp( int value ) { m_totalXp += value; }

	[SerializeField]
	public int m_totalRolls {get; private set; }
	[SerializeField]
	public int m_highestPrestigeRolls {get; private set; }
	public int c_currentPrestigeRolls {get; private set; }
	[SerializeField]
	public int m_highestTurnRolls {get; private set; }
	public int c_currentTurnRolls {get; private set; }
	public void AddRolls( int value ) 
	{ 
		m_totalRolls += value; 

		c_currentPrestigeRolls += value;
		if (m_highestPrestigeRolls < c_currentPrestigeRolls)
			m_highestPrestigeRolls = c_currentPrestigeRolls;

		c_currentTurnRolls += value;
		if (m_highestTurnRolls < c_currentTurnRolls)
			m_highestTurnRolls = c_currentTurnRolls;
	}

	[SerializeField]
	private int m_totalEyes;
	[SerializeField]
	private int m_highestTurnEyes;
	private int c_currentTurnEyes;
	public void AddEyes( int value )
	{
		m_totalEyes += value;
		c_currentTurnEyes += value;
		if (m_highestTurnEyes < c_currentTurnEyes)
			m_highestTurnEyes = c_currentTurnEyes;
	}
	public int GetTotalEyes() {return m_totalEyes;}
	public int GetHighestTurnEyes() {return m_highestTurnEyes;}

	[SerializeField]
	private int m_totalTurns;
	public void AddTurn(int value) {m_totalTurns += value;}
	public int GetTotalTurnsTaken() {return m_totalTurns;}

	[SerializeField]
	private int m_totalCombos;
	[SerializeField]
	private int m_highestTurnCombos;
	private int c_currentTurnCombos;
	public void AddCombo(int value) 
	{
		m_totalCombos += value;
		c_currentTurnCombos += value;
		if (m_highestTurnCombos < c_currentTurnCombos)
			m_highestTurnCombos = c_currentTurnCombos;
	}
	public int GetComboCount() {return m_totalCombos;}
	public int GetHighestTurnComos() {return m_highestTurnCombos;}

	[SerializeField]
	private int m_totalBoughtDice;

	public void AddBoughtDice(int value) {m_totalBoughtDice += value;}
	public int GetTotalBoughtDice() {return m_totalBoughtDice;}

	private void ResetAll()
	{
		m_totalGold = 0;
		m_highestTurnGold = 0;
		m_totalXp = 0;
		m_totalRolls = 0;
		m_highestTurnRolls = 0;
		m_totalEyes = 0;
		m_highestTurnEyes = 0;
		m_totalTurns = 0;
		m_totalCombos = 0;
		m_highestTurnCombos = 0;
		m_totalBoughtDice = 0;
		m_highestPrestigeGold = 0;
		m_highestPrestigeRolls = 0;
	}

	private void ResetCurrentPrestige()
	{
		c_currentPrestigeGold = 0;
		c_currentPrestigeRolls = 0;
	}

	public void ResetCurrentRoll()
	{
		c_currentTurnGold = 0;
		c_currentTurnRolls = 0;
		c_currentTurnEyes = 0;
		c_currentTurnCombos = 0;
	}

	public void ResetStats(bool keepTotals)
	{
		ResetCurrentRoll();
		ResetCurrentPrestige();
		if (!keepTotals)
			ResetAll();
	}
}

