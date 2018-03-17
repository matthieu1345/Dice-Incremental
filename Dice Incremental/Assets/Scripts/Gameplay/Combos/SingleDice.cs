using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSingleDice", menuName = "Combos/SingleDice", order = 1)]
public class SingleDice : ComboBase
{

	[SerializeField]
	private int m_diceValue;


	public override void CheckCombo(List<Dice> diceList)
	{
		foreach (Dice dice in diceList)
		{
			if (dice.GetRollValue() == m_diceValue)
				GiveReward(name, dice);
		}
	}

	protected override float GetMoneyMultiplication(Dice winningDice)
	{
		return winningDice.GetRollValue() * m_moneyReward;
	}

	protected override float GetXpMultiplication(Dice winningDice)
	{
		return winningDice.GetRollValue() * m_xpReward;
	}
}
