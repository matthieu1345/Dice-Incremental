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

	[SerializeField]
	private string m_readableName = "";
	public string GetReadableName() {return m_readableName;}

	[SerializeField]
	private string m_description = "";
	public string GetDescription() {return m_description;}

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

	[SerializeField]
	private EPerkRewardType m_rewardType;
	public EPerkRewardType GetRewardType() {return m_rewardType;}

	[SerializeField]
	protected EPerkStatType m_statType;

	//money reward
	[SerializeField]
	private int m_rewardAmount = 0;
	public int GetRewardAmount() {return m_rewardAmount;}

	[SerializeField]
	protected int m_statNumber = 0;

	//Combo reward
	[SerializeField]
	private ComboBase m_comboReward;
	public ComboBase GetComboReward() {return m_comboReward;}

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
		set 
		{
			if (m_rewardType != value)
				EditorUtility.SetDirty(this);
			m_rewardType = value;
		}
	}

	public int GUIRewardAmount
		
	{
		get {return m_rewardAmount;}
		set 
		{
			if (m_rewardAmount != value)
				EditorUtility.SetDirty(this);
			m_rewardAmount = value;
		}
	}

	public ComboBase GUIComboReward
	{
		get {return m_comboReward;}
		set 
		{
			if (m_comboReward != value)
				EditorUtility.SetDirty(this);			
			m_comboReward = value;
		}
	}

	public EPerkStatType GUIStatType
	{
		get { return m_statType;}
		set 
		{
			if (m_statType != value)	
				EditorUtility.SetDirty(this);
			m_statType = value;
		}
	}

	public int GUIStatNumber
	{
		get { return m_statNumber;}
		set 
		{
			if (m_statNumber != value)
				EditorUtility.SetDirty(this);			
			m_statNumber = value;
		}
	}

	public string GUIReadableName
	{
		get { return m_readableName;}
		set
		{
			if (m_readableName != value)
				EditorUtility.SetDirty(this);
			m_readableName = value;
		}
	}

	public string GUIDescription
	{
		get {return m_description;}
		set
		{
			if (m_description != value)
				EditorUtility.SetDirty(this);
			m_description = value;
		}
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
	bool GUIFoldout = false;
	bool rewardFoldout = false;
	bool typeFoldout = false;


	public override void OnInspectorGUI()
	{
		Perk perk = target as Perk;

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

		DrawGUIFoldout(perk);

		EditorGUILayout.Space();

		DrawRewardFoldout(perk);

		EditorGUILayout.Space();

		DrawTypeFoldout(perk);
	}

	public virtual void TypeOptions(Perk perk)
	{
		EditorGUILayout.LabelField("WARNING: This type of perk is invalid!", "WARNING: This type of perk is invalid!");
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("WARNING: This type of perk is invalid!", "WARNING: This type of perk is invalid!");
	}

	void DrawGUIFoldout(Perk perk)
	{
		GUIFoldout = EditorGUILayout.Foldout(GUIFoldout, "GUI Options");

		if (!GUIFoldout)
			return;

		EditorGUI.indentLevel++;

		perk.GUIReadableName = EditorGUILayout.TextField("GUI Perk Name: ", perk.GUIReadableName);
		EditorGUILayout.LabelField("GUI Perk Description:");
		perk.GUIDescription = EditorGUILayout.TextArea( perk.GUIDescription);

		EditorGUI.indentLevel--;
	}

	void DrawRewardFoldout(Perk perk)
	{
		rewardFoldout = EditorGUILayout.Foldout(rewardFoldout, "Reward Options");

		if (!rewardFoldout)
			return;

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

	void DrawTypeFoldout(Perk perk)
	{
		typeFoldout = EditorGUILayout.Foldout(typeFoldout, "Type Options");

		if (!typeFoldout)
			return;
		
		EditorGUI.indentLevel++;
		perk.GUIStatType = (Perk.EPerkStatType)EditorGUILayout.EnumPopup("Perk type:", perk.GUIStatType);

		EditorGUILayout.Space();

		TypeOptions(perk);

		EditorGUI.indentLevel--;
	}
}
#endif
