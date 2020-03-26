using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "NewPerk", menuName = "Perks/DiceRolledPerk", order = 3)]
public class DiceRolledPerk : Perk
{
	public override bool CheckPerk()
	{
		switch (m_statType)
		{
			case EPerkStatType.PST_Total:
				if (StatsManager.GetInstance().GetStats().TotalRolls >= m_statNumber)
					return true;
				break;
			case EPerkStatType.PST_SingleRoll:
				//TODO: single roll Dice Rolled perk
			case EPerkStatType.PST_SinglePrestige:
				//TODO: single prestige Dice Rolled perk
				Debug.LogError(this.name.ToString() + " has a not implemented stat type");
				break;
			default:
				Debug.LogError(this.name.ToString() + " has no stat type");
				break;
		}


		return false;
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(DiceRolledPerk), true)]
public class DiceRolledEditor : PerkEditor
{

	private bool m_showList = false;
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
	}

	public override void TypeOptions(Perk perk)
	{
		EditorGUILayout.LabelField("The perk will be unlocked when:");

		EditorGUI.indentLevel++;
		switch (perk.GUIStatType)
		{
			case Perk.EPerkStatType.PST_Total:
				EditorGUILayout.LabelField("The player their total Dice Rolled is above:");
				perk.GUIStatNumber = EditorGUILayout.IntField(" ", perk.GUIStatNumber);
				break;
			case Perk.EPerkStatType.PST_SingleRoll:
				EditorGUILayout.LabelField("The player their single roll Dice Rolled is above:");
				perk.GUIStatNumber = EditorGUILayout.IntField(" ", perk.GUIStatNumber);
				break;
			case Perk.EPerkStatType.PST_SinglePrestige:
				EditorGUILayout.LabelField("The player their game Dice Rolled is above:");
				perk.GUIStatNumber = EditorGUILayout.IntField(" ", perk.GUIStatNumber);
				break;
		}
		EditorGUI.indentLevel--;
	}
}
#endif