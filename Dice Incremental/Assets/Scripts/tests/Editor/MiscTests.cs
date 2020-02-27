using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MiscTests
{
	[Test]
	public void TestCounterIndex()
	{
		var gameObject = new GameObject();
		Counter testCounter = gameObject.AddComponent<Counter>();

		int value = 1234567890;

		Assert.AreEqual(5, testCounter.CalculateSpriteIndex(value, 5),0);
	}

}
