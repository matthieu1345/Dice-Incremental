using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatsData
{
	[SerializeField]
	private int m_totalGold;
	public int TotalGold { get => m_totalGold; private set => m_totalGold = value; }
	[SerializeField]
	private int m_highestPrestigeGold;
	public int HighestPrestigeGold { get => m_highestPrestigeGold; private set => m_highestPrestigeGold = value; }
	public int c_currentPrestigeGold {get; private set; }
	[SerializeField]
	private int m_highestTurnGold;
	public int HighestTurnGold { get => m_highestTurnGold; private set => m_highestTurnGold = value; }
	public int c_currentTurnGold {get; private set; }
	public void AddGold( int value ) 
	{
		TotalGold += value;

		c_currentPrestigeGold += value;
		if (HighestPrestigeGold < c_currentPrestigeGold)
			HighestPrestigeGold = c_currentPrestigeGold;

		c_currentTurnGold += value;
		if (HighestTurnGold < c_currentTurnGold)
			HighestTurnGold = c_currentTurnGold;
	}

	[SerializeField]
	private int m_totalXp;
	public int TotalXp { get => m_totalXp; private set => m_totalXp = value; }
	public void AddXp( int value ) { TotalXp += value; }

	[SerializeField]
	private int m_totalRolls;
	public int TotalRolls { get => m_totalRolls; private set => m_totalRolls = value; }
	[SerializeField]
	private int m_highestPrestigeRolls;
	public int HighestPrestigeRolls { get => m_highestPrestigeRolls; private set => m_highestPrestigeRolls = value; }
	public int c_currentPrestigeRolls {get; private set; }
	[SerializeField]
	private int m_highestTurnRolls;
	public int HighestTurnRolls { get => m_highestTurnRolls; private set => m_highestTurnRolls = value; }
	public int c_currentTurnRolls {get; private set; }

	public void AddRolls( int value ) 
	{ 
		TotalRolls += value; 

		c_currentPrestigeRolls += value;
		if (HighestPrestigeRolls < c_currentPrestigeRolls)
			HighestPrestigeRolls = c_currentPrestigeRolls;

		c_currentTurnRolls += value;
		if (HighestTurnRolls < c_currentTurnRolls)
			HighestTurnRolls = c_currentTurnRolls;
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
		TotalGold = 0;
		HighestTurnGold = 0;
		TotalXp = 0;
		TotalRolls = 0;
		HighestTurnRolls = 0;
		m_totalEyes = 0;
		m_highestTurnEyes = 0;
		m_totalTurns = 0;
		m_totalCombos = 0;
		m_highestTurnCombos = 0;
		m_totalBoughtDice = 0;
		HighestPrestigeGold = 0;
		HighestPrestigeRolls = 0;
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

