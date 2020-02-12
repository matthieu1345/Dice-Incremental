using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;




public class LevelManager : InstancedMonoBehaviour<LevelManager>
{
	[SerializeField]
	private float m_money;
	public float Money
	{
		get { return m_money; }
		private set
		{
			m_money = value;
			m_moneyChanged.Invoke((int)m_money);
		}
	}

	[SerializeField]
	private float m_xp;
	public float Xp
	{
		get { return m_xp; }
		private set
		{
			m_xp = value;
			m_xpChanged.Invoke((int)m_xp);
		}
	}


	[Serializable]
	public class ValueChangedEvent : UnityEvent<int>
	{
	}

	[SerializeField]
	public ValueChangedEvent m_moneyChanged = new ValueChangedEvent();
	[SerializeField]
	public ValueChangedEvent m_xpChanged = new ValueChangedEvent();
	public void AddMoney( float rewardValue )
	{
		StatsManager.GetInstance().RecievedMoney((int)rewardValue);
		Money += rewardValue;
	}

	public void AddXp( float rewardValue )
	{
		StatsManager.GetInstance().RecievedXp((int)rewardValue);
		Xp += rewardValue;
	}
	public int GetMoney() { return (int)m_money; }

	public bool Buy( float cost )
	{
		if ( Money >= cost )
		{
			Money -= cost;

			return true;
		}

		return false;
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
	}
}
