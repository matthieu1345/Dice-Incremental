using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum StatTypeEnum
{
	ST_Total,
	ST_Prestige,
	ST_Turn
}

[Serializable]
public class StatType
{
	[SerializeField]
	private StatTypeEnum m_type;
	public StatTypeEnum Type { get => m_type; set => m_type = value; }

	public bool NeedsReset(StatTypeEnum resetLevel)
	{
		return resetLevel <= Type;
	}
}

[CreateAssetMenu(fileName = "New Stat Object", menuName = "Stats/Base Stat", order = 1)]
public class Basestat : ScriptableObject
{
	[SerializeField]
	private string m_name;
	public string Name { get => m_name; protected set => m_name = value; }

	[SerializeField]
	private string m_description;
	public string Description { get => m_description; protected set => m_description = value; }

	[SerializeField]
	private Color m_UIChipColor = Color.white;
	public Color UIChipColor { get => m_UIChipColor; protected set => m_UIChipColor = value; }

	[SerializeField]
	protected StatType m_type = new StatType();
	public StatTypeEnum Type { get => m_type.Type; protected set => m_type.Type = value; }

	protected StatInstance m_instance = null;
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

	virtual public int GetPoinst()
	{ 
		return Instance.MaxValue;
	}

	virtual public StatValue GetValues()
	{
		return Instance.GetValues();
	}

	virtual public void LoadValues(StatValue stats)
	{
		Instance.LoadValues(stats);
	}

	virtual public void Reset(StatTypeEnum resetLevel)
	{
		if (Instance.GetType() != typeof(Basestat))
			Instance.Reset(resetLevel);
	}

	virtual public void ResetHighscore()
	{
		if (Instance.GetType() != typeof(Basestat))
			Instance.ResetHighscore();
	}

#if UNITY_EDITOR
	public string GUIReadableName 
		{ 
		get => m_name; 
		set 
			{ 
				if (m_name != value) EditorUtility.SetDirty(this);
				m_name = value;
			}
		}
	public string GUIReadableDescription 
	{
		get => m_description; 
		set 
		{
			if (m_description != value) EditorUtility.SetDirty(this);
			m_description = value; 
		}
	}
	public StatTypeEnum GUIReadableType 
	{ 
		get => m_type.Type;
		set 
		{ 
			if (m_type.Type != value) EditorUtility.SetDirty(this);
			m_type.Type = value;
		}
	}
	public StatInstance GUIReadableInstance { get => m_instance;}
	public Color GUIUIChipColor 
	{
		get => m_UIChipColor;
		set 
		{ 
			if (m_UIChipColor != value) EditorUtility.SetDirty(this);
			m_UIChipColor = value; 
		}
	}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(Basestat))]
public class BasestatEditor : Editor
{
	public override void OnInspectorGUI()
	{
		Basestat stat = target as Basestat;

		stat.GUIReadableName = EditorGUILayout.TextField("Name: ", stat.GUIReadableName);
		stat.GUIReadableDescription = EditorGUILayout.TextField("Description: ", stat.GUIReadableDescription);
		stat.GUIReadableType = (StatTypeEnum)EditorGUILayout.EnumPopup("Type:", stat.GUIReadableType);
		stat.GUIUIChipColor = EditorGUILayout.ColorField("Chip Color:", stat.GUIUIChipColor);

		if (stat.GUIReadableInstance)
		{
			EditorGUILayout.LabelField("Max Value: ", stat.GUIReadableInstance.MaxValue.ToString());
			EditorGUILayout.LabelField("Current Value: ", stat.GUIReadableInstance.CurrentValue.ToString());
		}
		else
		{
			EditorGUILayout.LabelField("There are currently no values for this stat!");
		}
	}
}
#endif