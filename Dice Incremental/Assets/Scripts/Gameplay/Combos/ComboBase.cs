using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class ComboBase : ScriptableObject
{

	[ReadOnly, SerializeField]
	private string m_guid = Guid.NewGuid().ToString();

	public string GetGuid() { return m_guid;}

#if UNITY_EDITOR
	public void GenerateNewGuid() { m_guid = Guid.NewGuid().ToString(); }
	public EComboRewardType GUIAwardType
	{
		get { return m_comboRewardType; }
		set { m_comboRewardType = value; }
	}
	public int GUIMoneyReward
	{
		get { return m_moneyReward; }
		set { m_moneyReward = value; }
	}
	public int GUIXpReward
	{
		get { return m_xpReward; }
		set { m_xpReward = value; }
	}
#endif

	public enum EComboRewardType
	{

		CRT_NotSet, //InValid! should never happen
		CRT_StaticAmount,
		CRT_ValueMultiplication
		
	};

	[SerializeField]
	protected EComboRewardType m_comboRewardType = EComboRewardType.CRT_StaticAmount;
	[SerializeField]
	protected int m_moneyReward;
	[SerializeField]
	protected int m_xpReward;

	public virtual void CheckCombo( List<Dice> diceList) { }

	protected virtual void GiveReward(string comboName, Dice winningdDice)
	{
#if DEBUG_COMBOS
		Debug.LogFormat("{0} has given you a reward!", comboName);
#endif

		switch ( m_comboRewardType )
		{

		case EComboRewardType.CRT_StaticAmount:
			LevelManager.GetInstance().AddMoney(m_moneyReward);
			LevelManager.GetInstance().AddXp(m_xpReward);

			break;
			
		case EComboRewardType.CRT_ValueMultiplication:
			LevelManager.GetInstance().AddMoney(GetMoneyMultiplication(winningdDice));
			LevelManager.GetInstance().AddXp(GetXpMultiplication(winningdDice));

			break;

		// reward not set should never happen and give same error as "default"
		// ReSharper disable once RedundantCaseLabel
		case EComboRewardType.CRT_NotSet:
		default:
				Debug.LogError(comboName + " has no reward type on dice: " + winningdDice.name);
			break;
		}
		
	}

	protected virtual float GetXpMultiplication(Dice winningDice) { return 0; }

	protected virtual float GetMoneyMultiplication(Dice winningDice) { return 0; }

}


#if UNITY_EDITOR
[CustomEditor(typeof(ComboBase), true)]
public class ComboEditor : Editor
{

	public override void OnInspectorGUI()
	{
		ComboBase combo = target as ComboBase;

		if (combo == null)
		{
			EditorGUILayout.LabelField("WARNING: something in the inspector went wrong!");
			EditorGUILayout.LabelField(" ", " ");
			EditorGUILayout.LabelField("WARNING: Reward type has not been set", "WARNING: Reward type has not been set");

			return;
		}

		EditorGUILayout.LabelField("Script", combo.GetType().ToString());
		EditorGUILayout.LabelField("Guid", combo.GetGuid());
		if (GUILayout.Button("Generate New Guid"))
		{
			((ComboBase)target).GenerateNewGuid();
		}

		EditorGUILayout.LabelField(" ", " ");

		combo.GUIAwardType = (ComboBase.EComboRewardType)EditorGUILayout.EnumPopup("Reward to give:", combo.GUIAwardType);

		switch ( combo.GUIAwardType )
		{
		case ComboBase.EComboRewardType.CRT_StaticAmount:
			combo.GUIMoneyReward = EditorGUILayout.IntField("Money to be given: ", combo.GUIMoneyReward);
			combo.GUIXpReward = EditorGUILayout.IntField("xp to be given:", combo.GUIXpReward);
			break;
		case ComboBase.EComboRewardType.CRT_ValueMultiplication:
			combo.GUIMoneyReward = EditorGUILayout.IntField("Value to be multiplied with for money: ", combo.GUIMoneyReward);
			combo.GUIXpReward = EditorGUILayout.IntField("Value to be multiplied with for xp: ", combo.GUIXpReward);
			break;
		default:
			EditorGUILayout.LabelField("WARNING: Reward type has not been set", "WARNING: Reward type has not been set");
			EditorGUILayout.LabelField(" ", " ");
			EditorGUILayout.LabelField("WARNING: Reward type has not been set", "WARNING: Reward type has not been set");

			break;
		}

		//base.OnInspectorGUI();
	}


}
#endif