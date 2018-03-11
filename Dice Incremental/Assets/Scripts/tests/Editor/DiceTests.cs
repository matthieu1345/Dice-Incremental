using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class DiceTests {

	[Test]
	public void DiceTestsPower()
	{
		// Use the Assert class to test conditions.
		DiceStats stats = new DiceStats();
		stats.TestConstructor(3, 6);
		const int expectedPower = 101;

		for (int i = 0; i < 100; i++)
			stats.AddPower();

		Assert.AreEqual(expectedPower, stats.GetPower(), 0);
	}

	[Test]
	public void DiceTestMagic()
	{
		DiceStats stats = new DiceStats();
		stats.TestConstructor(3, 6);
		const int expectedPower = 11;

		for (int i = 0; i < 100; i++)
			stats.AddPower();

		Assert.AreEqual(expectedPower, stats.GetMagic(), 0);
	}

	[Test]
	public void DiceTestGoal()
	{
		DiceStats stats = new DiceStats();
		stats.TestConstructor(3, 6);
		const int expectedGoal = 17;

		for (int i = 0; i < 100; i++)
			stats.AddPower();

		Assert.AreEqual(expectedGoal, stats.GetGoal(), 0);
	}

	[Test]
	public void DiceTestSides()
	{
		DiceStats stats = new DiceStats();
		stats.TestConstructor(3, 6);
		const int expectedSides = 4;

		for ( int i = 0; i < 100; i++ )
			stats.AddPower();

		Assert.AreEqual(expectedSides, stats.GetSides(), 0);
	}

}
