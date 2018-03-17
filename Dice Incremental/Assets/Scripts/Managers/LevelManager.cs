using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


public class LevelManager : InstancedMonoBehaviour<LevelManager>
{
	[SerializeField]
	private List<ComboBase> m_allCombos = new List<ComboBase>();

	[SerializeField]
	private float m_costMultiplierPerPower = 1;

	[SerializeField]
	private float m_basePowerCost = 1;

	[SerializeField]
	private float m_money;

	[SerializeField]
	private float m_xp;

	[Serializable]
	public class ValueChangedEvent : UnityEvent<int>
	{
	}

	[SerializeField]
	private ValueChangedEvent m_moneyChanged = new ValueChangedEvent();
	[SerializeField]
	private ValueChangedEvent m_xpChanged = new ValueChangedEvent();
	public void AddMoney( float rewardValue )
	{
		m_money += rewardValue;
		m_moneyChanged.Invoke((int)m_money);
	}

	public void AddXp( float rewardValue )
	{
		m_xp += rewardValue;
		m_xpChanged.Invoke((int)m_xp);
	}
	public int GetMoney() { return (int)m_money; }

	public float GetCostMultiplier() { return m_costMultiplierPerPower; }
	public float GetBaseCost() { return m_basePowerCost; }

	public void CheckCombos()
	{
		foreach ( ComboBase combo in m_allCombos )
		{
			combo.CheckCombo(DiceManager.GetInstance().GetDiceList());
		}
	}


	[MenuItem("Tools/Combo tools/Get all combo's")]
	private static void GetAllCombos()
	{
		
		ComboBase[] guid = Resources.LoadAll<ComboBase>("_DataAssets/DiceCombos");
		GetInstance().m_allCombos = guid.ToList();

	}

	public bool Buy( float cost )
	{
		if ( m_money >= cost )
		{
			m_money -= cost;

			return true;
		}

		return false;
	}


}
