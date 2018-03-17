using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DiceStats
{
	[Header("Current Data")]
	[ReadOnly, SerializeField]
	private int m_power = 1;
	[ReadOnly, SerializeField]
	private int m_sides = 3;
	[ReadOnly, SerializeField]
	private int m_goalSides = 6;
	[ReadOnly, SerializeField]
	private int m_magic;
	[ReadOnly, SerializeField]
	private int m_usedMagic;

	[Header("Starting Data")]
	[SerializeField]
	private int m_baseSides = 3;
	[SerializeField]
	private int m_firstSidesGoal = 6;

	public delegate void PowerAdded( int sides );

	public PowerAdded m_powerAdded;

	public int GetSides() { return m_sides; }
	public int GetPower() { return m_power; }
	public int GetGoal() { return m_goalSides; }
	public int GetMagic() { return m_magic; }

	public void TestConstructor( int baseSides, int firstGoal )
	{
		m_baseSides = baseSides;
		m_firstSidesGoal = firstGoal;
		Start();
	}

	// Use this for initialization
	public void Start ()
	{
		m_sides = m_baseSides;
		m_goalSides = m_firstSidesGoal;
	}

	public void AddSide()
	{
		m_sides++;
		m_power++;

		if (m_sides > m_goalSides)
		{
			m_sides = m_baseSides;
			m_goalSides++;
			m_magic++;
		}

		if (m_powerAdded != null)
		m_powerAdded.Invoke(m_sides);
	}
}
