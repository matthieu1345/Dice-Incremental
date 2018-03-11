using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSingleDice", menuName = "Combos/SingleDice", order = 0)]
public class DiceValue : ComboBase
{

	public override void CheckCombo( List<Dice> diceList )
	{
		foreach ( Dice dice in diceList )
		{
			GiveReward(name, dice);
		}
	}

	protected override float GetMoneyMultiplication( Dice winningDice )
	{
		return winningDice.GetRollValue() * m_moneyReward;
	}

	protected override float GetXpMultiplication( Dice winningDice )
	{
		return winningDice.GetRollValue() * m_xpReward;
	}

}
