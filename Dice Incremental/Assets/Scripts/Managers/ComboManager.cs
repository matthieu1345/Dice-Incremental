using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ComboManager : InstancedMonoBehaviour<ComboManager>
{
	[SerializeField]
	private Dictionary<string, ComboBase> m_allCombos = new Dictionary<string, ComboBase>();

	[SerializeField]
	private List<ComboBase> m_allComboObjects = new List<ComboBase>();

	[ReadOnly, SerializeField]
	private List<string> m_unlockedComboStrings;

	[SerializeField]
	private List<ComboBase> m_defaultUnlockedCombos = new List<ComboBase>();

	public List<string> GetUnlockedCombos() { return m_unlockedComboStrings; }

	public void LoadUnlockedCombos(List<string> combos) { m_unlockedComboStrings = combos; }

	protected override void Awake()
	{
		base.Awake();
		foreach (ComboBase combo in m_allComboObjects)
		{
			if (!GetInstance().m_allCombos.ContainsKey(combo.GetGuid()))
				GetInstance().m_allCombos.Add(combo.GetGuid(), combo);
		}
	}

	public void CheckCombos()
	{
		foreach (string combo in m_unlockedComboStrings)
		{
			m_allCombos[combo].CheckCombo(DiceManager.GetInstance().GetDiceList());
		}

		PerkManager.GetInstance().CheckPerks();
	}

	public void UnlockCombo(ComboBase combo)
	{
		if (!GetInstance().m_unlockedComboStrings.Contains(combo.GetGuid()))
			GetInstance().m_unlockedComboStrings.Add(combo.GetGuid());
	}

	public void Reset(bool keepUnlocks)
	{
		if (keepUnlocks)
			return;

		m_unlockedComboStrings.Clear();
		UnlockAllDefaultUnlockedCombos();
	}

	private void UnlockAllDefaultUnlockedCombos()
	{
		foreach (ComboBase combo in GetInstance().m_defaultUnlockedCombos)
		{
			m_unlockedComboStrings.Add(combo.GetGuid());
		}
	}

#if UNITY_EDITOR
	[MenuItem( "Tools/Combo tools/Get all combo's")]
	private static void GetAllCombos()
	{
		EditorUtility.SetDirty(GetInstance());
		GetInstance().m_allComboObjects.Clear();
		GetInstance().m_unlockedComboStrings.Clear();

		ComboBase[] list = Resources.LoadAll<ComboBase>("_DataAssets/DiceCombos");

		foreach (ComboBase combo in list)
		{
			GetInstance().m_allComboObjects.Add(combo);
		}

		GetInstance().UnlockAllDefaultUnlockedCombos();

	}
#endif
}
