using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatValue
{
	[SerializeField]
	public Basestat owner;
	[SerializeField]
	public int m_currentValue = 0;
	[SerializeField]
	public int m_maxValue = 0;
}

public class StatInstance : Basestat
{
	StatValue values;
	public int MaxValue { get => values.m_maxValue; private set => values.m_maxValue = value; }
	public int CurrentValue { get => values.m_currentValue; private set => values.m_currentValue = value;}

	public StatInstance()
	{
		m_instance = this;
	}

	public override StatValue GetValues()
	{
		return values;
	}

	public override void LoadValues(StatValue stats)
	{
		values = stats;
	}

	public void Init(Basestat original)
	{
		Name = original.Name;
		Description = original.Description;
		Type = original.Type;
		values = new StatValue();
		values.owner = original;
	}

	public override void Reset(StatTypeEnum resetLevel)
	{
		if (m_type.NeedsReset(resetLevel))
			CurrentValue = 0;
	}

	public override void ResetHighscore()
	{
		MaxValue = 0;
	}

	public static StatInstance operator +(StatInstance instance, int value)
	{
		instance.CurrentValue += value;
		if (instance.MaxValue < instance.CurrentValue)
			instance.MaxValue = instance.CurrentValue;

		return instance;
	}
}
