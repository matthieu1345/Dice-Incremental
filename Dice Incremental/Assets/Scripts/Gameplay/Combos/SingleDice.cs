﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "NewSingleDice", menuName = "Combos/SingleDice", order = 1)]
public class SingleDice : ComboBase
{

	[SerializeField]
	private int m_diceValue;
#if UNITY_EDITOR
	public int GUIRequiredValues
	{
		get {return m_diceValue; }
		set {m_diceValue = value; }
	}
#endif


	public override int CheckCombo(List<Dice> diceList, bool giveReward = true)
	{
		int combosFound = 0;
		foreach (Dice dice in diceList)
		{
			if (dice.GetRollValue() == m_diceValue)
			{
				if (giveReward)
				{
					GiveReward(name, dice);
				}
				combosFound++;
			}
		}

		return combosFound;
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(SingleDice), true)]
public class SingleEditor : ComboEditor
{

	public override void OnInspectorGUI()
	{
		SingleDice combo = target as SingleDice;

		base.OnInspectorGUI();

		if ( combo == null ) return;

		combo.GUIRequiredValues = EditorGUILayout.IntField("Dice Value: ", combo.GUIRequiredValues);
	}


}
#endif