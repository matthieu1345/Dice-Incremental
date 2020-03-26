using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PerkManager : InstancedMonoBehaviour<PerkManager>
{
    [SerializeField]
	private Dictionary<string, Perk> m_allPerks = new Dictionary<string, Perk>();

	[SerializeField]
	private List<Perk> m_allPerkObjects = new List<Perk>();

	[SerializeField]
	private List<string> m_unlockedPerkStrings = new List<string>();

	[SerializeField]
	private List<Perk> m_defaultUnlockedPerks = new List<Perk>();

	protected override void Awake() 
	{
		base.Awake();

		foreach (Perk perk in m_allPerkObjects)
		{
			if (!GetInstance().m_allPerks.ContainsKey(perk.GetGuid()))
				GetInstance().m_allPerks.Add(perk.GetGuid(), perk);
		}
	}

	void GiveAllRewards()
	{
		foreach (string perk in m_unlockedPerkStrings)
		{
			m_allPerks[perk].GiveReward();
		}
	}

	public void CheckPerks()
	{
		foreach (Perk perk in m_allPerkObjects)
		{
			if (perk.CheckPerk() && !m_unlockedPerkStrings.Contains(perk.GetGuid()))
			{
				m_unlockedPerkStrings.Add(perk.GetGuid());
				perk.GiveReward();
			}
		}
	}

	public void Reset(bool keepUnlocks)
	{
		if (keepUnlocks)
			return;

		m_unlockedPerkStrings.Clear();
		UnlockAllDefaultUnlockedPerks();
	}

	private void UnlockAllDefaultUnlockedPerks()
	{
		foreach (Perk perk in GetInstance().m_defaultUnlockedPerks)
		{
			m_unlockedPerkStrings.Add(perk.GetGuid());
		}
	}

#if UNITY_EDITOR
	[MenuItem( "Tools/Perk tools/Get all Perk's")]
	private static void GetAllCombos()
	{
		EditorUtility.SetDirty(GetInstance());
		GetInstance().m_allPerkObjects.Clear();
		GetInstance().m_unlockedPerkStrings.Clear();

		Perk[] list = Resources.LoadAll<Perk>("_DataAssets/Perks");

		foreach (Perk perk in list)
		{
			GetInstance().m_allPerkObjects.Add(perk);
		}

		GetInstance().UnlockAllDefaultUnlockedPerks();

	}
#endif
}
