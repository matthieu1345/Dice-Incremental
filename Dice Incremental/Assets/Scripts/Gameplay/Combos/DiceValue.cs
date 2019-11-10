﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDiceValue", menuName = "Combos/DiceValue", order = 0)]
public class DiceValue : ComboBase
{

	public override void CheckCombo( List<Dice> diceList )
	{
		foreach ( Dice dice in diceList )
		{
			GiveReward(name, dice);
		}
	}
}
