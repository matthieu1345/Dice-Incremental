using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatsData
{
	[SerializeField]
	private int m_totalGold;
	[SerializeField]
	private int m_highestTurnGold;
	private int c_currentTurnGold;
	public void AddGold( int value ) 
	{
		m_totalGold += value;
		c_currentTurnGold += value;
		if (m_highestTurnGold < c_currentTurnGold)
			m_highestTurnGold = c_currentTurnGold;
	}
	public int GetTotalGold() {return m_totalGold;}

	[SerializeField]
	private int m_totalXp;
	public void AddXp( int value ) { m_totalXp += value; }

	[SerializeField]
	private int m_totalRolls;
	[SerializeField]
	private int m_highestTurnRolls;
	private int c_currentTurnRolls;
	public void AddRolls( int value ) 
	{ 
		m_totalRolls += value; 
		c_currentTurnRolls += value;
		if (m_highestTurnRolls < c_currentTurnRolls)
			m_highestTurnRolls = c_currentTurnRolls;
	}
	public int GetTotalDiceRolled() {return m_totalRolls;}

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

	[SerializeField]
	private int m_totalBoughtDice;
	public void AddBoughtDice(int value) {m_totalBoughtDice += value;}
	public int GetTotalBoughtDice() {return m_totalBoughtDice;}

	public void ResetCurrentRoll()
	{
		c_currentTurnGold = 0;
		c_currentTurnRolls = 0;
		c_currentTurnEyes = 0;
		c_currentTurnCombos = 0;
	}
}
