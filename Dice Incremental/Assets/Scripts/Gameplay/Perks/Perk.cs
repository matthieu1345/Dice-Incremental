using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

[CreateAssetMenu(fileName = "NewPerk", menuName = "Perks/DefaultPerk", order = 99)]
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

	public enum EPerkStatType
	{
		PST_NotSet,
		PST_Total,
		PST_SingleRoll,
		PST_SinglePrestige
	}

	private EPerkRewardType m_rewardType;
	protected EPerkStatType m_statType;

	//money reward
	private int m_rewardAmount = 0;

	protected int m_statNumber = 0;

	//Combo reward
	private ComboBase m_comboReward;

	public void GiveReward()
	{
		switch ( m_rewardType )
		{
		case EPerkRewardType.PRT_Money:
			LevelManager.GetInstance().AddPerkMoney(m_rewardAmount);
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

	public virtual bool CheckPerk() { return false; }

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

	public EPerkStatType GUIStatType
	{
		get { return m_statType;}
		set { m_statType = value;}
	}

	public int GUIStatNumber
	{
		get { return m_statNumber;}
		set { m_statNumber = value;}
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
	bool rewardFoldout = false;
	bool typeFoldout = false;


	public override void OnInspectorGUI()
	{
		Perk perk = target as Perk;
		EditorUtility.SetDirty(perk);

		if ( perk == null )
		{
			EditorGUILayout.LabelField("WARNING: something in the inspector went wrong!");
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("WARNING: Reward type has not been set", "WARNING: Reward type has not been set");

			return;
		}

		EditorGUILayout.LabelField("Script", perk.GetType().ToString());
		EditorGUILayout.LabelField("Guid", perk.GetGuid());
		if (GUILayout.Button("Generate New Guid"))
		{
			((ComboBase)target).GenerateNewGuid();
		}

		EditorGUILayout.Space();

		rewardFoldout = EditorGUILayout.Foldout(rewardFoldout, "Reward Options");

		if (rewardFoldout)
		{
			EditorGUI.indentLevel++;
			perk.GUIAward = (Perk.EPerkRewardType)EditorGUILayout.EnumPopup("Reward to give:", perk.GUIAward);

			EditorGUILayout.Space();

			EditorGUILayout.LabelField("The following reward will be given:");
			EditorGUI.indentLevel++;
			switch ( perk.GUIAward)
			{
			case Perk.EPerkRewardType.PRT_Money:
				EditorGUILayout.LabelField("The player will start with more money:");
				perk.GUIRewardAmount = EditorGUILayout.IntField(" ", perk.GUIRewardAmount);
				break;
			case Perk.EPerkRewardType.PRT_Dice:
				EditorGUILayout.LabelField("The player will start with more dice:");
				perk.GUIRewardAmount = EditorGUILayout.IntField(" ", perk.GUIRewardAmount);
				break;
			case Perk.EPerkRewardType.PRT_Combo:
				EditorGUILayout.LabelField("The player will unlock the combo:");
				perk.GUIComboReward = (ComboBase)EditorGUILayout.ObjectField("", perk.GUIComboReward, typeof(ComboBase), false);
				break;
			case Perk.EPerkRewardType.PRT_Power:
				EditorGUILayout.LabelField("The players dice will start with more power:");
				perk.GUIRewardAmount = EditorGUILayout.IntField(" ", perk.GUIRewardAmount);
				break;

			default:
				EditorGUILayout.LabelField("WARNING: Reward type has not been set", "WARNING: Reward type has not been set");
				EditorGUILayout.Space();
				EditorGUILayout.LabelField("WARNING: Reward type has not been set", "WARNING: Reward type has not been set");

				break;
			}
			EditorGUI.indentLevel--;
			EditorGUI.indentLevel--;
		}

		EditorGUILayout.Space();

		typeFoldout = EditorGUILayout.Foldout(typeFoldout, "Type Options");

		if (typeFoldout)
		{
			EditorGUI.indentLevel++;
			perk.GUIStatType = (Perk.EPerkStatType)EditorGUILayout.EnumPopup("Perk type:", perk.GUIStatType);

			EditorGUILayout.Space();

			TypeOptions(perk);

			EditorGUI.indentLevel--;
		}
	}

	public virtual void TypeOptions(Perk perk)
	{
		EditorGUILayout.LabelField("WARNING: This type of perk is invalid!", "WARNING: This type of perk is invalid!");
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("WARNING: This type of perk is invalid!", "WARNING: This type of perk is invalid!");
	}
}
#endif
