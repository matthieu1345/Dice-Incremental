using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDiceSequence", menuName = "Combos/DiceSequence", order = 3)]
public class DiceSequence : ComboBase
{

	[SerializeField]
	private List<int> m_requiredValues = new List<int>();

	private class ValueCheck
	{

		public int m_value;
		public bool m_found = false;

		public ValueCheck(int value)
		{
			this.m_value = value;
		}

		public void Increment()
		{
			m_value++;
			m_found = false;
		}
	}

	private readonly SortedDictionary<int, List<Dice>> m_valueDictionary = new SortedDictionary<int, List<Dice>>();


	public override void CheckCombo(List<Dice> diceList)
	{
		GenerateValueDictionary(diceList);
		m_requiredValues.Sort();
		List<ValueCheck> required = new List<ValueCheck>();

		for (int i = 0; i < m_requiredValues.Count; i++)
		{
			required.Add(new ValueCheck(m_requiredValues[i]));
		}

		while ( m_valueDictionary.Keys.Last() >= required.Last().m_value )
		{

			Dictionary<int, int> valueCopy = m_valueDictionary.ToDictionary(k => k.Key, k => k.Value.Count);

			for ( int i = 0; i < required.Count; i++ )
			{

				if ( valueCopy.ContainsKey(required[i].m_value) )
				{
					required[i].m_found = true;

					valueCopy[required[i].m_value]--;

					if ( valueCopy[required[i].m_value] == 0 )
					{
						valueCopy.Remove(required[i].m_value);
					}
				}
				else
				{
					Reset(required);

					break;
				}

				if (i == required.Count - 1)
					PostCombo(required);
			}

			if ( m_valueDictionary.Count < 1 )
				break;

		}

	}

	private void GenerateValueDictionary(List<Dice> diceList)
	{
		m_valueDictionary.Clear();
		for ( int i = 0; i < diceList.Count; i++ )
		{
			int diceValue = diceList[i].GetRollValue();

			if (m_valueDictionary.ContainsKey(diceValue))
				m_valueDictionary[diceValue].Add(diceList[i]);
			else
			{
				m_valueDictionary.Add(diceValue, new List<Dice>());
				m_valueDictionary[diceValue].Add(diceList[i]);
			}

		}
	}

	private void Reset(List<ValueCheck> combo)
	{
		for ( int i = 0; i < combo.Count; i++ )
		{
			combo[i].Increment();
		}
	}

	private void PostCombo(List<ValueCheck> combo)
	{
		for ( int i = 0; i < combo.Count; i++ )
		{
			if ( m_comboRewardType == EComboRewardType.CRT_ValueMultiplication )
				GiveReward(name, m_valueDictionary[combo[i].m_value].Last());
			else if (i == 0)
				GiveReward(name, m_valueDictionary[combo[i].m_value].Last());

			m_valueDictionary[combo[i].m_value].RemoveAt(m_valueDictionary[combo[i].m_value].Count - 1);

			if ( m_valueDictionary[combo[i].m_value].Count == 0 )
				m_valueDictionary.Remove(combo[i].m_value);

			combo[i].m_found = false;
		}
	}
}
