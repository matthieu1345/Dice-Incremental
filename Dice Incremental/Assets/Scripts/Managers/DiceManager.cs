﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiceManager : InstancedMonoBehaviour<DiceManager>
{
	[SerializeField]
	private DiceUI m_uiFolder;
	[SerializeField]
	private Transform m_dicePrefab = null;

	[SerializeField]
	private int m_startingDice = 1;

	[SerializeField]
	private float m_diceCost = 10;

	[SerializeField]
	private float m_diceCostMultiplier = 2;

	[SerializeField]
	private float m_costMultiplierPerPower = 0.2f;

	[SerializeField]
	private float m_basePowerCost = 10;

	private List<Dice> m_allDice = new List<Dice>();

	public List<Dice> GetDiceList() { return m_allDice; }

	public delegate void RollEvent();

	public RollEvent m_rollEvent;

	public int GetDiceCount() { return m_allDice.Count; }

	private float GetNewDiceCost()
	{
		float newDiceCost = m_diceCost * (float)Math.Pow(m_diceCostMultiplier, GetDiceCount() - m_startingDice);

		m_uiFolder.SetDiceCost(newDiceCost);

		return newDiceCost;
	}

	public float GetPowerCostMultiplier() { return m_costMultiplierPerPower; }
	public float GetPowerBaseCost() { return m_basePowerCost; }


	public void AddDice(Dice dice)
	{
		m_allDice.Add(dice);
		dice.transform.SetParent(m_uiFolder.transform, false);
		dice.SetIndex(m_allDice.Count - 1);
		m_uiFolder.AddDiceObject(dice.gameObject);
		GetNewDiceCost();
	}

	public void RollAll()
	{
		StatsManager.GetInstance().TakenRoll();
		m_rollEvent.Invoke();
		ComboManager.GetInstance().CheckCombos();
	}

	public void AddPowerToAll()
	{
		for (int i = 0; i < m_allDice.Count; i++)
		{
			m_allDice[i].AddPower();
		}
	}

	public void CreateDice()
	{
		Instantiate(m_dicePrefab, new Vector3(0, 0, 0), Quaternion.identity);
	}

	public void LoadDice(DiceStats stats)
	{
		RemoveAllDice();

		Transform dice = Instantiate(m_dicePrefab, new Vector3(0, 0, 0), Quaternion.identity);

		dice.GetComponent<Dice>().LoadStats(stats);
	}

	protected override void Awake()
	{
		base.Awake();
		for ( int i = m_uiFolder.GetDiceCount(); i < m_startingDice; i++ )
		{
			CreateDice();
		}

		InputManager.GetInstance().m_buyDiceEvent.AddListener(BuyDice);
		InputManager.GetInstance().m_rollDiceEvent.AddListener(RollAll);
	}

	public void BuyDice()
	{
		if (LevelManager.GetInstance().Buy(GetNewDiceCost()))
		{
			StatsManager.GetInstance().BoughtDice();
			CreateDice();
		}
	}

	public void RemoveAllDice()
	{
		for ( int i = 0; i < m_allDice.Count; i++ )
		{
			Destroy(m_allDice[i].gameObject);
		}
		m_allDice.Clear();
		m_uiFolder.Reset();
	}
}
