using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DiceBoughtPerk : Perk
{
	public override bool CheckPerk()
	{
		switch (m_statType)
		{
			case EPerkStatType.PST_Total:
				if (StatsManager.GetInstance().GetStats().GetTotalBoughtDice() >= m_statNumber)
					return true;
				break;
			case EPerkStatType.PST_SingleRoll:
				//TODO: single roll dice bought perk
			case EPerkStatType.PST_SinglePrestige:
				//TODO: single prestige dice bought perk
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
[CustomEditor(typeof(DiceBoughtPerk), true)]
public class DiceBoughtPerkEditor : PerkEditor
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
				EditorGUILayout.LabelField("The player their total bought dice is above:");
				perk.GUIStatNumber = EditorGUILayout.IntField(" ", perk.GUIStatNumber);
				break;
			case Perk.EPerkStatType.PST_SingleRoll:
				EditorGUILayout.LabelField("The player their single roll bought dice is above:");
				perk.GUIStatNumber = EditorGUILayout.IntField(" ", perk.GUIStatNumber);
				break;
			case Perk.EPerkStatType.PST_SinglePrestige:
				EditorGUILayout.LabelField("The player their game bought dice is above:");
				perk.GUIStatNumber = EditorGUILayout.IntField(" ", perk.GUIStatNumber);
				break;
		}
		EditorGUI.indentLevel--;
	}
}
#endif