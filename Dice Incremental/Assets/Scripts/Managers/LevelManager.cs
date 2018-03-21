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
	//[SerializeField]
	//private List<ComboBase> m_allCombos = new List<ComboBase>();

	[SerializeField]
	private Dictionary<string, ComboBase> m_allCombos = new Dictionary<string, ComboBase>();

	[SerializeField]
	private List<ComboBase> m_defaultUnlockedCombos = new List<ComboBase>();
	[ReadOnly, SerializeField]
	private List<string> m_unlockedComboStrings = new List<string>();

	public List<string> GetUnlockedCombos() { return m_unlockedComboStrings;}

	public void LoadUnlockedCombos( List<string> combos ) { m_unlockedComboStrings = combos;}

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
		foreach ( string combo in m_unlockedComboStrings )
		{
			m_allCombos[combo].CheckCombo(DiceManager.GetInstance().GetDiceList());
		}
	}

#if UNITY_EDITOR
	[MenuItem("Tools/Combo tools/Get all combo's")]
	private static void GetAllCombos()
	{
		GetInstance().m_allCombos.Clear();
		GetInstance().m_unlockedComboStrings.Clear();

		ComboBase[] list = Resources.LoadAll<ComboBase>("_DataAssets/DiceCombos");

		foreach ( ComboBase combo in list)
		{
			GetInstance().m_allCombos.Add(combo.GetGuid(), combo);
		}

		foreach ( ComboBase combo in GetInstance().m_defaultUnlockedCombos)
		{
			GetInstance().m_unlockedComboStrings.Add(combo.GetGuid());
		}

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
		DiceManager.GetInstance().RemoveAllDice();
		SaveLoad.Load();
	}

}
