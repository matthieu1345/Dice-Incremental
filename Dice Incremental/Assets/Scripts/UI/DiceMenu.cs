using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DiceMenu : MonoBehaviour
{

	[ReadOnly, SerializeField]
	private Dice m_selectedDice = null;

	[ReadOnly, SerializeField]
	private float m_sideCost, m_maxSideCost, m_magicCost;

	[SerializeField]
	private Text m_singleText, m_maxText, m_magicText;
	
	public void SelectNewDice( Dice selectedDice )
	{
		m_selectedDice = selectedDice;
		CalculateCosts();
	}

	public void BuySide()
	{
		if ( LevelManager.GetInstance().Buy(m_sideCost) )
		{
			m_selectedDice.AddPower();
		}

		CalculateCosts();
	}

	public void BuyMaxSides()
	{
		if ( !LevelManager.GetInstance().Buy(m_maxSideCost) )
			return;

		int goalPower = m_selectedDice.GetGoal() - m_selectedDice.GetSides();

		for ( int i = 0; i < goalPower; i++ )
		{
			m_selectedDice.AddPower();
		}

		CalculateCosts();
	}

	public void BuyMagic()
	{
		if (!LevelManager.GetInstance().Buy(m_magicCost))
			return;

		int goalPower = m_selectedDice.GetGoal() - m_selectedDice.GetSides();

		for (int i = 0; i <= goalPower; i++)
		{
			m_selectedDice.AddPower();
		}

		CalculateCosts();
	}

	private void CalculateCosts()
	{
		m_sideCost = m_maxSideCost = m_magicCost = 0;
		m_sideCost = LevelManager.GetInstance().GetBaseCost() + LevelManager.GetInstance().GetCostMultiplier() * (m_selectedDice.GetPower() - 1) * LevelManager.GetInstance().GetBaseCost();
		
		for ( int i = m_selectedDice.GetSides(); i < m_selectedDice.GetGoal(); i++ )
		{
			m_maxSideCost += LevelManager.GetInstance().GetBaseCost() + LevelManager.GetInstance().GetBaseCost() * LevelManager.GetInstance().GetCostMultiplier() * (m_selectedDice.GetPower() - 1 + i - m_selectedDice.GetSides());
		}

		m_magicCost = m_maxSideCost;
		m_magicCost += LevelManager.GetInstance().GetBaseCost() + LevelManager.GetInstance().GetBaseCost() * LevelManager.GetInstance().GetCostMultiplier() * (m_selectedDice.GetPower() - 1 + m_selectedDice.GetGoal() - m_selectedDice.GetSides());

		m_singleText.text = "cost: " + m_sideCost.ToString("F") + " Gold";
		m_maxText.text = "cost: " + m_maxSideCost.ToString("F") + " Gold";
		m_magicText.text = "cost: " + m_magicCost.ToString("F") + " Gold";
	}


}
