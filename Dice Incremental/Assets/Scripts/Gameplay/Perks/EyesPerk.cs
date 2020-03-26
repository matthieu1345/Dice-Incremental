using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "NewPerk", menuName = "Perks/EyesPerk", order = 2)]
public class EyesPerk : Perk
{
	public override bool CheckPerk()
	{
		switch (m_statType)
		{
			case EPerkStatType.PST_Total:
				if (StatsManager.GetInstance().GetStats().GetTotalEyes() >= m_statNumber)
					return true;
				break;
			case EPerkStatType.PST_SingleRoll:
				//TODO: single roll Eyes perk
			case EPerkStatType.PST_SinglePrestige:
				//TODO: single prestige Eyes perk
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
[CustomEditor(typeof(EyesPerk), true)]
public class EyesEditor : PerkEditor
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
				EditorGUILayout.LabelField("The player their total rolled eyes is above:");
				perk.GUIStatNumber = EditorGUILayout.IntField(" ", perk.GUIStatNumber);
				break;
			case Perk.EPerkStatType.PST_SingleRoll:
				EditorGUILayout.LabelField("The player their single roll rolled eyes is above:");
				perk.GUIStatNumber = EditorGUILayout.IntField(" ", perk.GUIStatNumber);
				break;
			case Perk.EPerkStatType.PST_SinglePrestige:
				EditorGUILayout.LabelField("The player their game rolled eyes is above:");
				perk.GUIStatNumber = EditorGUILayout.IntField(" ", perk.GUIStatNumber);
				break;
		}
		EditorGUI.indentLevel--;
	}
}
#endif