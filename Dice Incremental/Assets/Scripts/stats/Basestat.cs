﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Object", menuName = "Stats/Base Stat", order = 3)]
public class Basestat : ScriptableObject
{
	[SerializeField]
	private string m_name;
	public string Name { get => m_name; protected set => m_name = value; }

	[SerializeField]
	private string m_description;
	public string Description { get => m_description; protected set => m_description = value; }
	
	public enum StatType
	{
		ST_Total,
		ST_Prestige,
		ST_Turn
	}

	[SerializeField]
	private StatType m_type;
	public StatType Type { get => m_type; protected set => m_type = value; }

	private StatInstance m_instance = null;
	public StatInstance Instance
	{
		get
		{
			if (m_instance)
				return m_instance;

			m_instance = ScriptableObject.CreateInstance<StatInstance>();
			m_instance.Init(this);
			return m_instance;
		}
		protected set => m_instance  = value;
	}

	public void AddPoints(int points)
	{
		Instance += points;
	}
}