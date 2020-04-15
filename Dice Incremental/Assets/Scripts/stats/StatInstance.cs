using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatInstance : Basestat
{
	int m_currentValue = 0;
	int m_maxValue = 0;

	StatInstance()
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
		m_maxValue = 0;
	}

	public static StatInstance operator +(StatInstance instance, int value)
	{
		instance.m_currentValue += value;
		if (instance.m_maxValue < instance.m_currentValue)
			instance.m_maxValue = instance.m_currentValue;

		return instance;
	}
}
