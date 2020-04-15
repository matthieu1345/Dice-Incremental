using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;




public class LevelManager : InstancedMonoBehaviour<LevelManager>
{
	[SerializeField]
	private int m_startingMoney;
	private int m_perkMoney;
	private int m_money;
	public int Money
	{
		get { return m_money; }
		private set
		{
			m_money = value;
			m_moneyChanged.Invoke(m_money);
		}
	}

	[SerializeField]
	private int m_xp;
	public int Xp
	{
		get { return m_xp; }
		private set
		{
			m_xp = value;
			m_xpChanged.Invoke(m_xp);
		}
	}

	[SerializeField]
	private int startingRolls;
	private int m_rolls;
	public int Rolls
	{
		get {return m_rolls;}
		private set
		{
			m_rolls = value;
			m_rollsChanged.Invoke(m_rolls);
		}
	}

	[SerializeField]
	private int m_rollBonusPoints;
	public int RollBonusPoints
	{
		get {return m_rollBonusPoints;}
		private set
		{
			m_rollBonusPoints = value;
			CheckRollBonusPoints();
			m_rollBonusPointsChanged.Invoke(m_rollBonusPoints);
		}
	}

	[SerializeField]
	private int m_baseRollBonusPointCost;
	[SerializeField]
	private float m_rollBonusPointCostMultiplier;
	private int m_bonusRolls = 0;
	[SerializeField]
	private int m_nextBonusRollCost;


	[Serializable]
	public class ValueChangedEvent : UnityEvent<int>
	{
	}

	[Serializable]
	public class ResetEvent : UnityEvent<bool>
	{
	}

	[SerializeField]
	public ValueChangedEvent m_moneyChanged = new ValueChangedEvent();
	[SerializeField]
	public ValueChangedEvent m_xpChanged = new ValueChangedEvent();
	[SerializeField]
	public ValueChangedEvent m_rollsChanged = new ValueChangedEvent();
	[SerializeField]
	public ValueChangedEvent m_rollBonusPointsChanged = new ValueChangedEvent();
	[SerializeField]
	public ResetEvent m_resetCalled = new ResetEvent();
	[SerializeField]
	StatGroup GoldStats;

	public void AddMoney( int rewardValue )
	{
		StatsManager.GetInstance().RecievedMoney(rewardValue);
		GoldStats.AddPoints(rewardValue);
		Money += rewardValue;
	}

	public void AddPerkMoney(int rewardValue)
	{
		AddMoney(rewardValue);
		m_perkMoney += rewardValue;
	}

	public void AddXp( int rewardValue )
	{
		StatsManager.GetInstance().RecievedXp(rewardValue);
		Xp += rewardValue;
	}

	public void AddRollBonusPoint(int rewardValue)
	{
		RollBonusPoints += rewardValue;
	}

	public int GetMoney() { return (int)m_money; }

	public bool Buy( int cost )
	{
		if ( Money >= cost )
		{
			Money -= cost;

			return true;
		}

		return false;
	}

	public void Roll()
	{
		Rolls--;
		if (Rolls == 0)
		{
			//Restart(true); //possibly show a restart message first.
		}
	}

	private void CheckRollBonusPoints()
	{
		m_nextBonusRollCost = CalculateNextRollCost();
		while ( RollBonusPoints >= m_nextBonusRollCost)
		{
			Rolls++;
			RollBonusPoints -= m_nextBonusRollCost;
			m_bonusRolls++;
			m_nextBonusRollCost = CalculateNextRollCost();
		}
	}

	int CalculateNextRollCost()
	{
		return Mathf.FloorToInt(m_baseRollBonusPointCost * (float)Math.Pow(m_rollBonusPointCostMultiplier, m_bonusRolls));
	}

	public void Save()
	{
		SaveLoad.Save();
	}

	public void Load()
	{
		DiceManager.GetInstance().RemoveAllDice();
		SaveLoad.Load();
	}

	public void LoadMana(ManaData data)
	{
		Money = data.m_money;
		Xp = data.m_xp;
		Rolls = data.m_rolls;
		RollBonusPoints = data.m_rollBonusPoints;
	}

	public void ResetMana(bool keepUnlocks)
	{
		Money = m_startingMoney;
		if (keepUnlocks)
			Money += m_perkMoney;

		if (!keepUnlocks)
			m_xp = 0;	//don't reset xp when you do a "regular" reset

		Rolls = startingRolls;

		RollBonusPoints = 0;

	}

	public void NewGamePlus()
	{
		Restart(true);
	}

	private void Restart(bool keepUnlocks)
	{
		m_resetCalled.Invoke(keepUnlocks);
	}

	protected override void Awake()
	{
		base.Awake();
		Restart(false);
	}
}
