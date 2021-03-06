﻿using NUnit.Framework;

public class DiceTests
{

	[Test]
	public void DiceTestsPower()
	{
		DiceStats stats = new DiceStats();
		stats.TestConstructor(3, 6);
		const int expectedPower = 101;

		for (int i = 0; i < 100; i++)
			stats.AddSide();

		Assert.AreEqual(expectedPower, stats.GetPower(), 0);
	}

	[Test]
	public void DiceTestMagic()
	{
		DiceStats stats = new DiceStats();
		stats.TestConstructor(3, 6);
		const int expectedPower = 11;

		for (int i = 0; i < 100; i++)
			stats.AddSide();

		Assert.AreEqual(expectedPower, stats.GetMagic(), 0);
	}

	[Test]
	public void DiceTestGoal()
	{
		DiceStats stats = new DiceStats();
		stats.TestConstructor(3, 6);
		const int expectedGoal = 17;

		for (int i = 0; i < 100; i++)
			stats.AddSide();

		Assert.AreEqual(expectedGoal, stats.GetGoal(), 0);
	}

	[Test]
	public void DiceTestSides()
	{
		DiceStats stats = new DiceStats();
		stats.TestConstructor(3, 6);
		const int expectedSides = 4;

		for ( int i = 0; i < 100; i++ )
			stats.AddSide();

		Assert.AreEqual(expectedSides, stats.GetSides(), 0);
	}

}
