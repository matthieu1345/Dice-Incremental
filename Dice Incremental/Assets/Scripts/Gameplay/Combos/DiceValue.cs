using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDiceValue", menuName = "Combos/DiceValue", order = 0)]
public class DiceValue : ComboBase
{

	public override int CheckCombo( List<Dice> diceList )
	{
		int combosFound = 0;
		foreach ( Dice dice in diceList )
		{
			StatsManager.GetInstance().CompletedCombo();
			GiveReward(name, dice);
			combosFound++;
		}

		return combosFound;
	}
}
