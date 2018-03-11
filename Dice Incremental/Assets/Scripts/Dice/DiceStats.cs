using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class DiceStats : MonoBehaviour
{
	[Header("Current Data")]
	[ReadOnly, SerializeField]
	private int m_power = 1;
	[ReadOnly, SerializeField]
	private int m_sides = 3;
	[ReadOnly, SerializeField]
	private int m_goalSides = 6;
	[ReadOnly, SerializeField]
	private int m_magic = 0;

	[Header("Starting Data")]
	[SerializeField]
	private int m_baseSides = 3;
	[SerializeField]
	int m_firstSidesGoal = 6;

	public class MyEvent : UnityEvent<int>
	{ }

	public MyEvent m_powerAdded = null;

	public int GetSides()
	{
		return m_sides;
	}

	void Awake()
	{
		if (m_powerAdded == null)
			m_powerAdded = new MyEvent();
	}

	// Use this for initialization
	void Start ()
	{
		m_sides = m_baseSides;
		m_goalSides = m_firstSidesGoal;
		DiceManager.m_instance.AddDice(this);
	}

	public void AddPower()
	{
		m_sides++;
		m_power++;

		if (m_sides > m_goalSides)
		{
			m_sides = m_baseSides;
			m_goalSides++;
			m_magic++;
		}

		m_powerAdded.Invoke(m_sides);
	}
}
