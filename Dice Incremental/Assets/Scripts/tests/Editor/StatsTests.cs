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
}
