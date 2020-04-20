using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatInstance : Basestat
{
	int m_currentValue = 0;
	int m_maxValue = 0;
	public int MaxValue { get => m_maxValue; private set => m_maxValue = value; }

	public StatInstance()
	{
		m_instance = this;
	}

	public void Init(Basestat original)
	{
		Name = original.Name;
		Description = original.Description;
		Type = original.Type;
	}

	public override void Reset(StatTypeEnum resetLevel)
	{
		if (m_type.NeedsReset(resetLevel))
			m_currentValue = 0;
	}

	public override void ResetHighscore()
	{
		MaxValue = 0;
	}

	public static StatInstance operator +(StatInstance instance, int value)
	{
		instance.m_currentValue += value;
		if (instance.MaxValue < instance.m_currentValue)
			instance.MaxValue = instance.m_currentValue;

		return instance;
	}
}
