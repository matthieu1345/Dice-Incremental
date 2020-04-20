using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "NewPerk", menuName = "Perks/TurnsTakenPerk", order = 4)]
public class TurnsTakenPerk : Perk
{
	public override bool CheckPerk()
	{
		switch (m_statType)
		{
			case EPerkStatType.PST_Total:
				if (StatsManager.GetInstance().GetStats().TotalTurns >= m_statNumber)
					return true;
				break;
			case EPerkStatType.PST_SinglePrestige:
				//TODO: single prestige Turns Taken perk
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
[CustomEditor(typeof(TurnsTakenPerk), true)]
public class TurnsTakenEditor : PerkEditor
{
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
				EditorGUILayout.LabelField("The player their total Turns Taken is above:");
				perk.GUIStatNumber = EditorGUILayout.IntField(" ", perk.GUIStatNumber);
				break;
			case Perk.EPerkStatType.PST_SingleRoll:
				EditorGUILayout.LabelField("The player their single roll Turns Taken is above:");
				EditorGUILayout.Space();
				EditorGUILayout.LabelField("You should not use this one! it makes no sense cause this is always 1!");
				EditorGUILayout.Space();
				perk.GUIStatNumber = EditorGUILayout.IntField(" ", perk.GUIStatNumber);
				break;
			case Perk.EPerkStatType.PST_SinglePrestige:
				EditorGUILayout.LabelField("The player their game TurnsTaken is above:");
				perk.GUIStatNumber = EditorGUILayout.IntField(" ", perk.GUIStatNumber);
				break;
		}
		EditorGUI.indentLevel--;
	}
}
#endif