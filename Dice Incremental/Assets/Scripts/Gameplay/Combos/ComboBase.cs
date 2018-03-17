using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboBase : ScriptableObject
{

	public enum ERewardType
	{

		RT_NotSet, //InValid! should never happen
		RT_StaticAmount,
		RT_ValueMultiplication
		
	};

	[SerializeField]
	protected ERewardType m_rewardType = ERewardType.RT_StaticAmount;
	[SerializeField]
	protected int m_moneyReward;
	[SerializeField]
	protected int m_xpReward;

	public virtual void CheckCombo( List<Dice> diceList) { }

	public virtual void GiveReward(string comboName, Dice winningdDice)
	{
#if DEBUG_COMBOS
		Debug.LogFormat("{0} has given you a reward!", comboName);
#endif

		switch ( m_rewardType )
		{

		case ERewardType.RT_StaticAmount:
			LevelManager.GetInstance().AddMoney(m_moneyReward);
			LevelManager.GetInstance().AddXp(m_xpReward);

			break;
			
		case ERewardType.RT_ValueMultiplication:
			LevelManager.GetInstance().AddMoney(GetMoneyMultiplication(winningdDice));
			LevelManager.GetInstance().AddXp(GetXpMultiplication(winningdDice));

			break;

		// reward not set should never happen and give same error as "default"
		// ReSharper disable once RedundantCaseLabel
		case ERewardType.RT_NotSet:
		default:
				Debug.LogError(comboName + " has no reward type on dice: " + winningdDice.name);
			break;
		}
	}

	protected virtual float GetXpMultiplication(Dice winningDice) { return 0; }

	protected virtual float GetMoneyMultiplication(Dice winningDice) { return 0; }

}
