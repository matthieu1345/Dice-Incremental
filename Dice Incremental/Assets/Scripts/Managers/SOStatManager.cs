using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SOStatManager : InstancedMonoBehaviour<SOStatManager>
{
	[SerializeField]
	private List<Basestat> m_allStatObjects = new List<Basestat>();

	[SerializeField]
	private List<StatGroup> m_allStatGroups = new List<StatGroup>();

	public List<StatValue> GetSaveValues()
	{
		List<StatValue> Output = new List<StatValue>();

		foreach (Basestat stat in m_allStatObjects)
		{
			Output.Add(stat.GetValues());
		}

		return Output;
	}

	public void LoadSaveValues(List<StatValue> input)
	{
		foreach (StatValue stat in input)
		{
			stat.owner.LoadValues(stat);
		}
	}

	//has to be added because unity buttons don't take functions with enum parameters
	public void NewGamePlus()
	{
		ResetStats(StatTypeEnum.ST_Prestige);
	}

	public void ResetStats(StatTypeEnum ResetLevel)
	{
		foreach (Basestat stat in m_allStatObjects)
		{
			stat.Reset(ResetLevel);
		}
	}

#if UNITY_EDITOR
	[MenuItem( "Tools/Stat tools/Get all Stats")]
	private static void GetAllStats()
	{
		EditorUtility.SetDirty(GetInstance());
		GetInstance().m_allStatObjects.Clear();

		Basestat[] list = Resources.LoadAll<Basestat>("_DataAssets/Stats");

		foreach (Basestat stat in list)
		{
			GetInstance().m_allStatObjects.Add(stat);
		}

	}

	[MenuItem( "Tools/Stat tools/Get all Stat groups")]
	private static void GetAllStatGroups()
	{
		EditorUtility.SetDirty(GetInstance());
		GetInstance().m_allStatGroups.Clear();

		StatGroup[] list = Resources.LoadAll<StatGroup>("_DataAssets/Stats");

		foreach (StatGroup group in list)
		{
			GetInstance().m_allStatGroups.Add(group);
		}

	}

	[MenuItem("Tools/Stat tools/Get all Stat groups and singles")]
	private static void GetAllStatsAndGroups()
	{
		GetAllStatGroups();
		GetAllStats();
	}
#endif
}
