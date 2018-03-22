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
#endif

	protected enum EComboRewardType
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
		base.OnInspectorGUI();

		EditorGUILayout.LabelField(" ", " ");

		if (GUILayout.Button("Generate New Guid"))
		{
			((ComboBase)target).GenerateNewGuid();
		}

	}


}
#endif