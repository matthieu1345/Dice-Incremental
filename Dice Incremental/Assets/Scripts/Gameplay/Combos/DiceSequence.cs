using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "NewDiceSequence", menuName = "Combos/DiceSequence", order = 3)]
public class DiceSequence : ComboBase
{

	[SerializeField]
	private List<int> m_requiredValues = new List<int>();
#if UNITY_EDITOR
	public List<int> GUIRequiredValues
	{
		get { return m_requiredValues; }
		set { m_requiredValues = value; }
	}
#endif


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


	public override int CheckCombo(List<Dice> diceList, bool giveReward = true)
	{
		int combosFound = 0;

		GenerateValueDictionary(diceList);
		m_requiredValues.Sort();
		List<ValueCheck> requiredValues = new List<ValueCheck>();

		for (int i = 0; i < m_requiredValues.Count; i++)
		{
			requiredValues.Add(new ValueCheck(m_requiredValues[i]));
		}

		while ( m_valueDictionary.Keys.Last() >= requiredValues.Last().m_value )
		{

			Dictionary<int, int> valueCopy = m_valueDictionary.ToDictionary(k => k.Key, k => k.Value.Count);

			for ( int i = 0; i < requiredValues.Count; i++ )
			{

				if ( valueCopy.ContainsKey(requiredValues[i].m_value) )
				{
					requiredValues[i].m_found = true;

					valueCopy[requiredValues[i].m_value]--;

					if ( valueCopy[requiredValues[i].m_value] == 0 )
					{
						valueCopy.Remove(requiredValues[i].m_value);
					}
				}
				else
				{
					Reset(requiredValues);

					break;
				}

				if (i == requiredValues.Count - 1)
				{
					PostCombo(requiredValues, giveReward);

					combosFound++;
				}
			}

			if ( m_valueDictionary.Count < 1 )
				break;

		}

		return combosFound;
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

	private void PostCombo(List<ValueCheck> combo, bool giveReward)
	{
		for ( int i = 0; i < combo.Count; i++ )
		{
			if (giveReward)
			{
				if ( m_comboRewardType == EComboRewardType.CRT_ValueMultiplication )
					GiveReward(name, m_valueDictionary[combo[i].m_value].Last());
				else if (i == 0)
					GiveReward(name, m_valueDictionary[combo[i].m_value].Last());
			}

			m_valueDictionary[combo[i].m_value].RemoveAt(m_valueDictionary[combo[i].m_value].Count - 1);

			if ( m_valueDictionary[combo[i].m_value].Count == 0 )
				m_valueDictionary.Remove(combo[i].m_value);

			combo[i].m_found = false;
		}
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(DiceSequence), true)]
public class SequenceEditor : ComboEditor
{

	private bool m_showList = false;
	public override void OnInspectorGUI()
	{
		DiceSequence combo = target as DiceSequence;


		base.OnInspectorGUI();

		m_showList = EditorGUILayout.Foldout(m_showList, "Dice Values needed:");

		if ( !m_showList || combo == null ) return;

		for ( int i = 0; i < combo.GUIRequiredValues.Count; i++ )
		{
			combo.GUIRequiredValues[i] = EditorGUILayout.IntField("Dice Value #" + i + ": ", combo.GUIRequiredValues[i]);
		}
	}


}
#endif