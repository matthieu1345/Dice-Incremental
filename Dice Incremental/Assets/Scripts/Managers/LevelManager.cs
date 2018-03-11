using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


public class LevelManager : InstancedMonoBehaviour<LevelManager>
{

	[SerializeField]
	private List<ComboBase> m_allCombos = new List<ComboBase>();

	[SerializeField]
	private float m_money = 0;

	[SerializeField]
	float m_xp = 0;

	public void AddMoney( float rewardValue ) { m_money += rewardValue; }
	public void AddXp( float rewardValue ) { m_xp += rewardValue; }

	public void CheckCombos()
	{
		foreach ( ComboBase combo in m_allCombos )
		{
			combo.CheckCombo(DiceManager.GetInstance().GetDiceList());
		}
	}


	[MenuItem("Tools/Combo tools/Get all combo's")]
	static void GetAllCombos()
	{
		
		var guid = Resources.LoadAll<ComboBase>("_DataAssets/DiceCombos");
		GetInstance().m_allCombos = guid.ToList();

	}
}
