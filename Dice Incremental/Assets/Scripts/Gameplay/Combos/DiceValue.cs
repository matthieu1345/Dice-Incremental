using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDiceValue", menuName = "Combos/DiceValue", order = 0)]
public class DiceValue : ComboBase
{

	public override int CheckCombo( List<Dice> diceList , bool giveReward = true)
	{
		int combosFound = 0;
		foreach ( Dice dice in diceList )
		{
			if (giveReward)
			{
				GiveReward(name, dice);
			}
			combosFound++;
		}

		return combosFound;
	}
}
