using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceMenu : MonoBehaviour
{
	[ReadOnly, SerializeField]
	private Dice m_selectedDice;

	[ReadOnly, SerializeField]
	private int m_sideCost, m_maxSideCost, m_magicCost;

	[SerializeField]
	private TextMeshProUGUI m_singleText;

	[SerializeField]
	private TextMeshProUGUI m_maxText;

	[SerializeField]
	private TextMeshProUGUI m_magicText;

	public void SelectNewDice( Dice selectedDice )
	{
		m_selectedDice = selectedDice;
		CalculateCosts();
	}

	public void SelectNextDice()
	{
		int newIndex;

		if (m_selectedDice.GetIndex() == DiceManager.GetInstance().GetDiceList().Count - 1 )
			newIndex = 0;
		else
			newIndex = m_selectedDice.GetIndex() + 1;

		SelectNewDice(DiceManager.GetInstance().GetDiceList()[newIndex]);
	}

	public void SelectPreviousDice()
	{
		int newIndex;

		if ( m_selectedDice.GetIndex() == 0 )
			newIndex = DiceManager.GetInstance().GetDiceList().Count - 1;
		else
			newIndex = m_selectedDice.GetIndex() - 1;

		SelectNewDice(DiceManager.GetInstance().GetDiceList()[newIndex]);
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

		float powerBaseCost = DiceManager.GetInstance().GetPowerBaseCost();
		float powerMultiplier = DiceManager.GetInstance().GetPowerCostMultiplier();
		float powerBaseAmount = DiceManager.GetInstance().GetPowerBaseAmount();

		m_sideCost = Mathf.FloorToInt(powerBaseCost + powerMultiplier * ((int)m_selectedDice.GetPower() - powerBaseAmount) * powerBaseCost);
		
		for ( int i = m_selectedDice.GetSides(); i < m_selectedDice.GetGoal(); i++ )
		{
			m_maxSideCost += Mathf.FloorToInt(powerBaseCost + powerBaseCost * powerMultiplier * ((int)m_selectedDice.GetPower() - powerBaseAmount + i - m_selectedDice.GetSides()));
		}

		m_magicCost = m_maxSideCost;
		m_magicCost += Mathf.FloorToInt(powerBaseCost + powerBaseCost * powerMultiplier * ((int)m_selectedDice.GetPower() - powerBaseAmount + m_selectedDice.GetGoal() - m_selectedDice.GetSides()));

		m_singleText.text = "cost: " + m_sideCost.ToString("F") + " Gold";
		m_maxText.text = "cost: " + m_maxSideCost.ToString("F") + " Gold";
		m_magicText.text = "cost: " + m_magicCost.ToString("F") + " Gold";
	}

	private void Start()
	{
		InputManager.GetInstance().m_selectNextDiceEvent.AddListener(SelectNextDice);
		InputManager.GetInstance().m_selectPreviousDiceEvent.AddListener(SelectPreviousDice);
	}

}
