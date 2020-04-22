using NUnit.Framework;
using UnityEngine;

public class StatsTests
{
    [Test]
	public void TestDontResetTotal()
	{
		StatType testStat = new StatType();
		testStat.Type = StatTypeEnum.ST_Total;
		StatTypeEnum resetLevel = StatTypeEnum.ST_Turn;

		Assert.AreEqual(false, testStat.NeedsReset(resetLevel));
	}

	[Test]
	public void TestResetTurn()
	{
		StatType testStat = new StatType();
		testStat.Type = StatTypeEnum.ST_Turn;
		StatTypeEnum ResetLevel = StatTypeEnum.ST_Total;

		Assert.AreEqual(true, testStat.NeedsReset(ResetLevel));
	}

	[Test]
	public void TestPerkValueHigher()
	{
		int testStatValue = 100;
		int testGoalValue = 50;

		Basestat testStat = ScriptableObject.CreateInstance<Basestat>();
		testStat.AddPoints(testStatValue);
		Perk testPerk = ScriptableObject.CreateInstance<Perk>();
		testPerk.SetTestValues(testStat, testGoalValue);

		Assert.AreEqual(true, testPerk.CheckPerk());
	}

	[Test]
	public void TestPerkValueSame()
	{
		int testStatValue = 50;
		int testGoalValue = 50;

		Basestat testStat = ScriptableObject.CreateInstance<Basestat>();
		testStat.AddPoints(testStatValue);
		Perk testPerk = ScriptableObject.CreateInstance<Perk>();
		testPerk.SetTestValues(testStat, testGoalValue);

		Assert.AreEqual(true, testPerk.CheckPerk());
	}

	[Test]
	public void TestPerkValueLower()
	{
		int testStatValue = 50;
		int testGoalValue = 100;

		Basestat testStat = ScriptableObject.CreateInstance<Basestat>();
		testStat.AddPoints(testStatValue);
		Perk testPerk = ScriptableObject.CreateInstance<Perk>();
		testPerk.SetTestValues(testStat, testGoalValue);

		Assert.AreEqual(false, testPerk.CheckPerk());
	}
}
