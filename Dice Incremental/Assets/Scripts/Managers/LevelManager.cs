using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


#if UNITY_EDITOR
using UnityEditor;
#endif

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
	private ValueChangedEvent m_moneyChanged = new ValueChangedEvent();
	[SerializeField]
	private ValueChangedEvent m_xpChanged = new ValueChangedEvent();
	public void AddMoney( float rewardValue )
	{
		Money += rewardValue;
	}

	public void AddXp( float rewardValue )
	{
		Xp += rewardValue;
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

#if UNITY_EDITOR
	[MenuItem("Tools/Combo tools/Get all combo's")]
	private static void GetAllCombos()
	{
		
		ComboBase[] guid = Resources.LoadAll<ComboBase>("_DataAssets/DiceCombos");
		GetInstance().m_allCombos = guid.ToList();

	}
#endif

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
		SaveLoad.Load();
	}

}
