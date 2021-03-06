﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "NewMultiDice", menuName = "Combos/MultiDice", order = 2)]
public class MultiDice : ComboBase
{

	[SerializeField]
	private int[] m_diceValues;
#if UNITY_EDITOR
	public int[] GUIRequiredValues
	{
		get { return m_diceValues; }
		set { m_diceValues = value; }
	}
#endif

	private class ValuePair
	{

		public int m_value;
		public bool m_found;

		public ValuePair(int value)
		{
			m_value = value;
			m_found = false;
		}
	}

	private List<ValuePair> m_valueList;

	private List<Dice> m_diceList;

	public override int CheckCombo( List<Dice> diceList, bool b_giveReward = true)
	{
		int combosFound = 0;
		ResetList();

		for ( int i = 0; i < diceList.Count; i++ )
		{
			Dice dice = diceList[i];
			bool hasEverything = true;
			bool counted = false;

			// go over each value needed and check if the dice has this value
			foreach ( ValuePair pair in m_valueList )
			{
				if ( !pair.m_found && pair.m_value == dice.GetRollValue() && !counted ) 
				{
					//check if the value is not already found, the dice has the current value, and is not counted yet
					counted = true;
					pair.m_found = true;
					m_diceList.Add(dice);
				}

				if ( !pair.m_found )
					hasEverything = false;
			}

			//if every value is used, give the reward once
			if ( hasEverything )
			{
				combosFound++;
				GiveReward(b_giveReward);
			}
		}

		return combosFound;
	}


	private void ResetList()
	{
		m_valueList = new List<ValuePair>();
		m_diceList = new List<Dice>();

		foreach (int value in m_diceValues)
		{
			m_valueList.Add(new ValuePair(value));
		}

	}

	private void GiveReward(bool b_giveReward)
	{
		if (!b_giveReward)
		{
			ResetList();
			return;
		}

		if (m_comboRewardType == EComboRewardType.CRT_ValueMultiplication)
		{
			foreach (Dice winningDice in m_diceList)
			{
				GiveReward(name, winningDice);
			}
		}
		else
		{
			GiveReward(name, m_diceList[0]);
		}

		ResetList();
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(MultiDice), true)]
public class MultiEditor : ComboEditor
{

	private bool m_showList = false;
	public override void OnInspectorGUI()
	{
		MultiDice combo = target as MultiDice;


		base.OnInspectorGUI();

		m_showList = EditorGUILayout.Foldout(m_showList, "Dice Values needed:");

		if (!m_showList || combo == null) return;

		for (int i = 0; i < combo.GUIRequiredValues.Length; i++)
		{
			combo.GUIRequiredValues[i] = EditorGUILayout.IntField("Dice Value #" + i + ": ", combo.GUIRequiredValues[i]);
		}
	}


}
#endif