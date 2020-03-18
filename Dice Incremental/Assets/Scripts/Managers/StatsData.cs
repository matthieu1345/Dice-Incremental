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
	public int GetTotalGold() {return m_totalGold;}

	[SerializeField]
	private int m_totalXp;
	public void AddXp( int value ) { m_totalXp += value; }

	[SerializeField]
	private int m_totalRolls;
	public void AddRolls( int value ) { m_totalRolls += value; }

	[SerializeField]
	private int m_totalEyes;
	public void AddEyes( int value ) { m_totalEyes += value; }
	public int GetTotalEyes() {return m_totalEyes;}

	[SerializeField]
	private int m_totalTurns;
	public void AddTurn(int value) {m_totalTurns += value;}

	[SerializeField]
	private int m_totalCombos;
	public void AddCombo(int value) {m_totalCombos += value;}

	[SerializeField]
	private int m_totalBoughtDice;
	public void AddBoughtDice(int value) {m_totalBoughtDice += value;}
	public int GetTotalBoughtDice() {return m_totalBoughtDice;}
}
