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
	[ReadOnly, SerializeField]
	private string m_guid = Guid.NewGuid().ToString();

	public string GetGuid() { return m_guid;}

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
	private int m_rewardAmount = 0;

	//Combo reward
	private ComboBase m_comboReward;

	public void GiveReward()
	{
		switch ( m_rewardType )
		{
		case EPerkRewardType.PRT_Money:
			LevelManager.GetInstance().AddMoney(m_rewardAmount);
			break;

		case EPerkRewardType.PRT_Dice:
			for (int i = 0; i < m_rewardAmount; i++)
			{
				DiceManager.GetInstance().AddPerkDice();
			}
			break;

		case EPerkRewardType.PRT_Combo:
			ComboManager.GetInstance().UnlockCombo(m_comboReward);
			break;

		case EPerkRewardType.PRT_Power:
			for (int i = 0; i < m_rewardAmount; i++)
			{
				DiceManager.GetInstance().AddPerkPower();
			}
			break;

		default:
			Debug.LogWarningFormat("Perk {0} has set a wrong reward!", name);
			break;
		}
	}

#if UNITY_EDITOR
	// ReSharper disable ConvertToAutoPropertyWhenPossible
	// ReSharper disable ConvertToAutoProperty
	public EPerkRewardType GUIAward
	{
		get {return m_rewardType;}
		set {m_rewardType = value;}
	}

	public int GUIRewardAmount
		
	{
		get {return m_rewardAmount;}
		set {m_rewardAmount = value;}
	}

	public ComboBase GUIComboReward
	{
		get {return m_comboReward;}
		set {m_comboReward = value;}
	}
	// ReSharper restore ConvertToAutoProperty
	// ReSharper restore ConvertToAutoPropertyWhenPossible

	public void GenerateNewGuid() { m_guid = Guid.NewGuid().ToString(); }
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
		EditorGUILayout.LabelField("Guid", perk.GetGuid());
		if (GUILayout.Button("Generate New Guid"))
		{
			((ComboBase)target).GenerateNewGuid();
		}

		EditorGUILayout.LabelField(" ", " ");

		perk.GUIAward = (Perk.EPerkRewardType)EditorGUILayout.EnumPopup("Reward to give:", perk.GUIAward);

		EditorGUILayout.LabelField(" ", " ");

		EditorGUILayout.LabelField("The following reward will be given:");
		switch ( perk.GUIAward)
		{
		case Perk.EPerkRewardType.PRT_Money:
			perk.GUIRewardAmount = EditorGUILayout.IntField("The player will start with more money:", perk.GUIRewardAmount);
			break;
		case Perk.EPerkRewardType.PRT_Dice:
			perk.GUIRewardAmount = EditorGUILayout.IntField("The player will start with more dice:", perk.GUIRewardAmount);
			break;
		case Perk.EPerkRewardType.PRT_Combo:
			perk.GUIComboReward = (ComboBase)EditorGUILayout.ObjectField("The player will unlock the combo:", perk.GUIComboReward, typeof(ComboBase), false);
			break;
		case Perk.EPerkRewardType.PRT_Power:
			perk.GUIRewardAmount = EditorGUILayout.IntField("The players dice will start with more power:", perk.GUIRewardAmount);
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
