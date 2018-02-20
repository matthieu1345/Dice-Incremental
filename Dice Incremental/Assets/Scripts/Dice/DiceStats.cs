using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiceStats : MonoBehaviour
{
	[SerializeField]
	private int power = 1;
	[SerializeField]
	private int sides = 3;
	private int baseSides = 3;
	[SerializeField]
	private int goalSides = 6;
	[SerializeField]
	private int magic = 0;

	public class MyEvent : UnityEvent<int>
	{ }

	public MyEvent powerAdded = null;

	public int GetSides()
	{
		return sides;
	}

	void Awake()
	{
		if (powerAdded == null)
			powerAdded = new MyEvent();
	}

	// Use this for initialization
	void Start ()
	{
		sides = baseSides;
		DiceManager.instance.AddDice(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddPower()
	{
		sides++;
		power++;

		if (sides >= goalSides)
		{
			sides = baseSides;
			goalSides++;
			magic++;
		}

		powerAdded.Invoke(sides);
	}
}
