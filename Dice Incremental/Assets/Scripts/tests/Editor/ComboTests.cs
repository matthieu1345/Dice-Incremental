using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ComboTests
{
	[Test]
	public void ComboTestValue()
	{
		ComboBase comboType = ScriptableObject.CreateInstance<DiceValue>();
		const int diceCount = 5;
		const int expectedCombos = diceCount;
		var gameObject = new GameObject();

		List<Dice> diceList = new List<Dice>();
		for (int i = 0; i < diceCount; i++)
		{
			diceList.Add(gameObject.AddComponent<Dice>());
			diceList[i].TestRollValueSetter(i);
		}

		Assert.AreEqual(expectedCombos, comboType.CheckCombo(diceList, false),0);
	}

	[Test]
	public void ComboTestSingleDice()
	{
		SingleDice comboType = ScriptableObject.CreateInstance<SingleDice>();
		const int diceCount = 5;
		const int expectedCombos = 1;
		var gameObject = new GameObject();
		comboType.GUIRequiredValues = 1;

		List<Dice> diceList = new List<Dice>();
		for (int i = 0; i < diceCount; i++)
		{
			diceList.Add(gameObject.AddComponent<Dice>());
			diceList[i].TestRollValueSetter(i);
		}

		Assert.AreEqual(expectedCombos, comboType.CheckCombo(diceList, false),0);
	}

	// tests for 6x six in the dice array [6, 6, 6, 6, 6, 0, 1, 2, 3, 4]
	[Test]
	public void ComboTestMultiDiceSimple()
	{
		MultiDice comboType = ScriptableObject.CreateInstance<MultiDice>();
		const int diceCount = 5;
		const int expectedCombos = 1;
		var gameObject = new GameObject();
		int[] comboValues = {6,6,6};
		comboType.GUIRequiredValues = comboValues;

		List<Dice> diceList = new List<Dice>();
		for (int i = 0; i < 5; i++)
		{
			diceList.Add(gameObject.AddComponent<Dice>());
			diceList[i].TestRollValueSetter(6);
		}
		for (int i = 0; i < diceCount; i++)
		{
			diceList.Add(gameObject.AddComponent<Dice>());
			diceList[i+5].TestRollValueSetter(i);
		}

		Assert.AreEqual(expectedCombos, comboType.CheckCombo(diceList, false),0);
	}

	// tests for 1 and 4 in the dice array [0, 1, 2, 3, 0, 1, 2, 3, 4]
	[Test]
	public void ComboTestMultiDiceAdvanced()
	{
		MultiDice comboType = ScriptableObject.CreateInstance<MultiDice>();
		const int diceCount = 5;
		const int expectedCombos = 1;
		var gameObject = new GameObject();
		int[] comboValues = {1,4};
		comboType.GUIRequiredValues = comboValues;

		List<Dice> diceList = new List<Dice>();
		for (int i = 0; i < 4; i++)
		{
			diceList.Add(gameObject.AddComponent<Dice>());
			diceList[i].TestRollValueSetter(i);
		}
		for (int i = 0; i < diceCount; i++)
		{
			diceList.Add(gameObject.AddComponent<Dice>());
			diceList[i+4].TestRollValueSetter(i);
		}

		Assert.AreEqual(expectedCombos, comboType.CheckCombo(diceList, false),0);
	}

	[Test]
	public void ComboTestDiceSequenceTwoOfAKind()
	{
		DiceSequence comboType = ScriptableObject.CreateInstance<DiceSequence>();
		const int diceCount = 5;
		const int expectedCombos = 2;
		var gameObject = new GameObject();
		List<int> comboValues = new List<int>();
		comboValues.Add(1);
		comboValues.Add(1);
		comboType.GUIRequiredValues = comboValues;

		List<Dice> diceList = new List<Dice>();
		for (int i = 0; i < diceCount; i++)
		{
			diceList.Add(gameObject.AddComponent<Dice>());
			diceList[i].TestRollValueSetter(3);
		}

		Assert.AreEqual(expectedCombos, comboType.CheckCombo(diceList, false),0);
	}

	[Test]
	public void ComboTestDiceSequenceStraightTwo()
	{
		DiceSequence comboType = ScriptableObject.CreateInstance<DiceSequence>();
		const int diceCount = 5;
		const int expectedCombos = 2;
		var gameObject = new GameObject();
		List<int> comboValues = new List<int>();
		comboValues.Add(1);
		comboValues.Add(2);
		comboType.GUIRequiredValues = comboValues;

		List<Dice> diceList = new List<Dice>();
		for (int i = 0; i < diceCount; i++)
		{
			diceList.Add(gameObject.AddComponent<Dice>());
			diceList[i].TestRollValueSetter(i);
		}

		Assert.AreEqual(expectedCombos, comboType.CheckCombo(diceList, false),0);
	}
}
