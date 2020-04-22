using System;
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
	private int m_perkDice = 0;
	private int m_startingPower = 1;
	private int m_perkPower = 0;
	private int TotalStartingDice()
	{
		return m_startingDice + m_perkDice;
	}

	[SerializeField]
	private float m_diceCost = 10;

	[SerializeField]
	private float m_diceCostMultiplier = 2;

	[SerializeField]
	private float m_costMultiplierPerPower = 0.2f;

	[SerializeField]
	private float m_basePowerCost = 10;

	[SerializeField]
	StatGroup DiceBoughtStat;

	private List<Dice> m_allDice = new List<Dice>();

	public List<Dice> GetDiceList() { return m_allDice; }

	public UnityEvent m_rollEvent = new UnityEvent();

	public int GetDiceCount() { return m_allDice.Count; }

	private int GetNewDiceCost()
	{
		int newDiceCost = Mathf.FloorToInt(m_diceCost * (float)Math.Pow(m_diceCostMultiplier, GetDiceCount() - TotalStartingDice()));

		m_uiFolder.SetDiceCost(newDiceCost);

		return newDiceCost;
	}

	public float GetPowerCostMultiplier() { return m_costMultiplierPerPower; }
	public float GetPowerBaseCost() { return m_basePowerCost; }
	public float GetPowerBaseAmount() { return m_startingPower + m_perkPower; }

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
		if (LevelManager.GetInstance().Rolls <= 0)
			return; // we can't roll anymore if there's no rolls left!
		
		SOStatManager.GetInstance().ResetStats(StatTypeEnum.ST_Turn);
		m_rollEvent.Invoke();
		ComboManager.GetInstance().CheckCombos();

	}

	public void AddPerkDice()
	{
		m_perkDice++;
		CreateDice();
	}

	public void AddPerkPower()
	{
		m_perkPower++;
		AddPowerToAll();
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
	}

	private void Start()
	{
		InputManager.GetInstance().m_buyDiceEvent.AddListener(BuyDice);
		InputManager.GetInstance().m_rollDiceEvent.AddListener(RollAll);
	}

	public void BuyDice()
	{
		if (LevelManager.GetInstance().Buy(GetNewDiceCost()))
		{
			DiceBoughtStat.AddPoints(1);
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

	public void ResetDice(bool keepUnlocks)
	{
		if (!keepUnlocks)
		{
			m_perkDice = 0;
			m_perkPower = 0;
		}

		RemoveAllDice();

		for ( int i = m_uiFolder.GetDiceCount(); i < TotalStartingDice(); i++ )
		{
			CreateDice();
		}
	}
}
