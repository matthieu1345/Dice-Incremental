using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Group", menuName = "Stats/StatGroup", order = 2)]
public class StatGroup : ScriptableObject
{
	[SerializeField]
	Basestat[] stats;

	public void AddPoints(int point)
	{
		foreach (Basestat stat in stats)
		{
			stat.AddPoints(point);
		}
	}

}
