using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMultiDice", menuName = "Combos/MultiDice", order = 2)]
public class MultiDice : ComboBase
{

	[SerializeField]
	private int[] m_diceValues;

	private class ValuePair
	{

		public int m_value;
		public bool m_found;

		public ValuePair(int value)
		{
			m_value = value;
			m_found = false;
		}
	}

	private List<ValuePair> m_valueList;

	private List<Dice> m_diceList;

	public override void CheckCombo( List<Dice> diceList )
	{
		ResetList();

		foreach ( Dice dice in diceList )
		{

			bool hasEverything = true;
			bool counted = false;

			foreach ( ValuePair pair in m_valueList )
			{
				if ( !pair.m_found && pair.m_value == dice.GetRollValue() && !counted)
				{
					counted = true;
					pair.m_found = true;
					m_diceList.Add(dice);
				}

				if ( !pair.m_found )
					hasEverything = false;
			}

			if ( hasEverything ) { GiveReward(); }

		}
	}


	public void ResetList()
	{
		m_valueList = new List<ValuePair>();
		m_diceList = new List<Dice>();

		foreach (int value in m_diceValues)
		{
			m_valueList.Add(new ValuePair(value));
		}

	}

	public void GiveReward()
	{

		if (m_comboRewardType == EComboRewardType.CRT_ValueMultiplication)
		{
			foreach (Dice winningDice in m_diceList)
			{
				GiveReward(name, winningDice);
			}
		}
		else
		{
			GiveReward(name, m_diceList[0]);
		}

		ResetList();
	}
}
