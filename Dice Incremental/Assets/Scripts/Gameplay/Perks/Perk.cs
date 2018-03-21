using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

[CreateAssetMenu(fileName = "NewPerk", menuName = "Perks/DefaultPerk", order = 0)]
public class Perk : ScriptableObject
{

	public enum EPerkRewardType
	{
		PRT_NotSet,
		PRT_Money,
		PRT_Dice,
		PRT_Combo,
		PRT_Power
	}

	private EPerkRewardType m_rewardType;

	//money reward
	private int m_moneyReward = 0;

	//Combo reward
	private ComboBase m_comboReward;

	public void GiveReward()
	{
		switch ( m_rewardType )
		{
		case EPerkRewardType.PRT_Money:

			break;
		case EPerkRewardType.PRT_Dice:

			break;
		case EPerkRewardType.PRT_Combo:

			break;
		case EPerkRewardType.PRT_Power:

			break;

		default:

			Debug.LogWarningFormat("Perk {0} has set a wrong reward!", name);

			break;
		}
	}

#if UNITY_EDITOR
	// ReSharper disable ConvertToAutoProperty

	public EPerkRewardType GUIAward
	{
		get {return m_rewardType;}
		set {m_rewardType = value;}
	}

	public int GUIMoneyReward
	{
		get {return m_moneyReward;}
		set {m_moneyReward = value;}
	}

	public ComboBase GUIComboReward
	{
		get {return m_comboReward;}
		set {m_comboReward = value;}
	}

	// ReSharper restore ConvertToAutoProperty
#endif
}


#if UNITY_EDITOR
[CustomEditor(typeof(Perk))]
public class PerkEditor : Editor
{

	public override void OnInspectorGUI()
	{
		Perk perk = target as Perk;

		if ( perk == null )
		{
			EditorGUILayout.LabelField("WARNING: something in the inspector went wrong!");
			EditorGUILayout.LabelField(" ", " ");
			EditorGUILayout.LabelField("WARNING: Reward type has not been set", "WARNING: Reward type has not been set");

			return;
		}

		EditorGUILayout.LabelField("Script", perk.GetType().ToString());

		EditorGUILayout.LabelField(" ", " ");

		perk.GUIAward = (Perk.EPerkRewardType)EditorGUILayout.EnumPopup("Reward to give:", perk.GUIAward);

		EditorGUILayout.LabelField(" ", " ");

		EditorGUILayout.LabelField("The following reward will be given:");
		switch ( perk.GUIAward)
		{
		case Perk.EPerkRewardType.PRT_Money:
			perk.GUIMoneyReward = EditorGUILayout.IntField("The player will start with more money:", perk.GUIMoneyReward);
			break;
		case Perk.EPerkRewardType.PRT_Dice:
			EditorGUILayout.LabelField("The player will start with 1 more dice");
			break;
		case Perk.EPerkRewardType.PRT_Combo:
			perk.GUIComboReward = (ComboBase)EditorGUILayout.ObjectField("The player will unlock the combo:", perk.GUIComboReward, typeof(ComboBase), false);
			break;
		case Perk.EPerkRewardType.PRT_Power:
			EditorGUILayout.LabelField("The players dice will start with 1 more power");
			break;

		default:
			EditorGUILayout.LabelField("WARNING: Reward type has not been set", "WARNING: Reward type has not been set");
			EditorGUILayout.LabelField(" ", " ");
			EditorGUILayout.LabelField("WARNING: Reward type has not been set", "WARNING: Reward type has not been set");

			break;
		}
	}

}
#endif
