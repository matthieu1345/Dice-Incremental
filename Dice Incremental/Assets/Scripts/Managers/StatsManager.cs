﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


[Serializable]
public class StatsManager : InstancedMonoBehaviour<StatsManager>
{
	[SerializeField]
	private StatsData m_stats;

	public void LoadStats(StatsData stats) { m_stats = stats; }

	public StatsData GetStats() { return m_stats; }

	public void CompletedCombo()
	{
		m_stats.AddCombo(1);
	}

	public void BoughtDice()
	{
		m_stats.AddBoughtDice(1);
	}

	public void ResetCurrentRoll()
	{
		m_stats.ResetCurrentRoll();
	}

	public void ResetStats(bool keepTotals)
	{
		m_stats.ResetStats(keepTotals);
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(StatsManager))]
public class StatsManagerEditor : Editor
{
	Dictionary<string, bool> foldouts = new Dictionary<string, bool>();

	void AddFoldout(string key)
	{
		if (!foldouts.ContainsKey(key))
			foldouts.Add(key, false);
	}

	StatsManager stats; 
	public override void OnInspectorGUI()
	{
		stats = target as StatsManager;

		
		EditorGUILayout.LabelField("By Record Length", EditorStyles.boldLabel);
		TotalStats();
		SingleTurnStats();
		PrestigeStats();

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("By Stat Type", EditorStyles.boldLabel);

		GoldStats();
		XPStats();
		RollsStats();
		EyesStats();
		TurnsStats();
		ComboStats();
		DiceCountStats();
	}

	void TotalStats()
	{
		AddFoldout("TotalStats");
		foldouts["TotalStats"] = EditorGUILayout.Foldout(foldouts["TotalStats"], "Totals");

		if (!foldouts["TotalStats"])
			return;

		EditorGUI.indentLevel++;

		TotalGold();
		TotalXP();
		TotalRolls();
		TotalEyes();
		TotalTurns();
		TotalCombos();
		TotalDiceBought();

		EditorGUI.indentLevel--;
	}

	void SingleTurnStats()
	{
		AddFoldout("SingleTurnStats");
		foldouts["SingleTurnStats"] = EditorGUILayout.Foldout(foldouts["SingleTurnStats"], "Single Turn");

		if (!foldouts["SingleTurnStats"])
			return;

		EditorGUI.indentLevel++;

		SingleTurnGold();
		SingleTurnXP();
		SingleTurnRolls();
		SingleTurnEyes();
		SingleTurnCombos();
		SingleTurnBoughtDice();

		EditorGUI.indentLevel--;
	}

	void PrestigeStats()
	{
		AddFoldout("PrestigeStats");
		foldouts["PrestigeStats"] = EditorGUILayout.Foldout(foldouts["PrestigeStats"], "Prestige");

		if (!foldouts["PrestigeStats"])
			return;

		EditorGUI.indentLevel++;

		PrestigeGold();
		PrestigeXP();
		PrestigeRolls();
		PrestigeEyes();
		PrestigeTurns();
		PrestigeCombos();
		PrestigeBoughtDice();

		EditorGUI.indentLevel--;
	}

	void GoldStats()
	{
		AddFoldout("GoldStats");
		foldouts["GoldStats"] = EditorGUILayout.Foldout(foldouts["GoldStats"], "Gold");

		if (!foldouts["GoldStats"])
			return;

		EditorGUI.indentLevel++;

		TotalGold();
		PrestigeGold();
		SingleTurnGold();

		EditorGUI.indentLevel--;
	}

	void XPStats()
	{
		AddFoldout("XPStats");
		foldouts["XPStats"] = EditorGUILayout.Foldout(foldouts["XPStats"], "XP");

		if (!foldouts["XPStats"])
			return;

		EditorGUI.indentLevel++;

		TotalXP();
		PrestigeXP();
		SingleTurnXP();

		EditorGUI.indentLevel--;
	}

	void RollsStats()
	{
		AddFoldout("RollsStats");
		foldouts["RollsStats"] = EditorGUILayout.Foldout(foldouts["RollsStats"], "Rolls");

		if (!foldouts["RollsStats"])
			return;

		EditorGUI.indentLevel++;

		TotalRolls();
		PrestigeRolls();
		SingleTurnRolls();

		EditorGUI.indentLevel--;
	}

	void EyesStats()
	{
		AddFoldout("EyesStats");
		foldouts["EyesStats"] = EditorGUILayout.Foldout(foldouts["EyesStats"], "Eyes");

		if (!foldouts["EyesStats"])
			return;

		EditorGUI.indentLevel++;

		TotalEyes();
		PrestigeEyes();
		SingleTurnEyes();

		EditorGUI.indentLevel--;
	}

	void TurnsStats()
	{
		AddFoldout("TurnsStats");
		foldouts["TurnsStats"] = EditorGUILayout.Foldout(foldouts["TurnsStats"], "Turns");

		if (!foldouts["TurnsStats"])
			return;

		EditorGUI.indentLevel++;

		TotalTurns();
		PrestigeTurns();

		EditorGUI.indentLevel--;
	}

	void ComboStats()
	{
		AddFoldout("ComboStats");
		foldouts["ComboStats"] = EditorGUILayout.Foldout(foldouts["ComboStats"], "Combo");

		if (!foldouts["ComboStats"])
			return;

		EditorGUI.indentLevel++;

		TotalCombos();
		PrestigeCombos();
		SingleTurnCombos();

		EditorGUI.indentLevel--;
	}

	void DiceCountStats()
	{
		AddFoldout("DiceCountStats");
		foldouts["DiceCountStats"] = EditorGUILayout.Foldout(foldouts["DiceCountStats"], "Dice Count");

		if (!foldouts["DiceCountStats"])
			return;

		EditorGUI.indentLevel++;

		TotalDiceBought();
		PrestigeBoughtDice();
		SingleTurnBoughtDice();

		EditorGUI.indentLevel--;
	}

//Gold
	void TotalGold()
	{
		EditorGUILayout.LabelField("Total Gold:", stats.GetStats().TotalGold.ToString());
	}

	void SingleTurnGold()
	{
		EditorGUILayout.LabelField("Single Turn Gold:", stats.GetStats().HighestTurnGold.ToString());
	}

	void PrestigeGold()
	{
		EditorGUILayout.LabelField("Prestige Gold:", stats.GetStats().HighestPrestigeGold.ToString());
	}

//XP
	void TotalXP()
	{
		EditorGUILayout.LabelField("Total XP:", stats.GetStats().TotalXp.ToString());
	}

	void SingleTurnXP()
	{
		EditorGUILayout.LabelField("Single Turn XP:", stats.GetStats().HighestTurnXP.ToString());
	}

	void PrestigeXP()
	{
		EditorGUILayout.LabelField("Prestige XP:", stats.GetStats().HighestPrestigeXP.ToString());
	}

//Rolls
	void TotalRolls()
	{
		EditorGUILayout.LabelField("Total Rolls:", stats.GetStats().TotalRolls.ToString());
	}

	void SingleTurnRolls()
	{
		EditorGUILayout.LabelField("Single Turn Rolls:", stats.GetStats().HighestTurnRolls.ToString());
	}

	void PrestigeRolls()
	{
		EditorGUILayout.LabelField("Prestige Rolls:", stats.GetStats().HighestPrestigeRolls.ToString());
	}

//Eyes
	void TotalEyes()
	{
		EditorGUILayout.LabelField("Total Eyes:", stats.GetStats().TotalEyes.ToString());
	}

	void SingleTurnEyes()
	{
		EditorGUILayout.LabelField("Single Turn Eyes:", stats.GetStats().HighestTurnEyes.ToString());
	}

	void PrestigeEyes()
	{
		EditorGUILayout.LabelField("Prestige Eyes:", stats.GetStats().HighestPrestigeEyes.ToString());
	}

//Turns
	void TotalTurns()
	{
		EditorGUILayout.LabelField("Total Turns:", stats.GetStats().TotalTurns.ToString());
	}

	void PrestigeTurns()
	{
		EditorGUILayout.LabelField("Prestige Turns:", stats.GetStats().HighestPrestigeTurns.ToString());
	}

//Combos
	void TotalCombos()
	{
		EditorGUILayout.LabelField("Total Combos:", stats.GetStats().TotalCombos.ToString());
	}

	void SingleTurnCombos()
	{
		EditorGUILayout.LabelField("Single Turn Combos:", stats.GetStats().HighestTurnCombos.ToString());
	}

	void PrestigeCombos()
	{
		EditorGUILayout.LabelField("Prestige Combos:", stats.GetStats().HighestPrestigeCombos.ToString());
	}

//Dice Count
	void TotalDiceBought()
	{
		EditorGUILayout.LabelField("Total Dice Bought:", stats.GetStats().TotalBoughtDice.ToString());
	}

	void SingleTurnBoughtDice()
	{
		EditorGUILayout.LabelField("Single Turn Dice Bought:", stats.GetStats().HighestTurnBoughtDice.ToString());
	}

	void PrestigeBoughtDice()
	{
		EditorGUILayout.LabelField("Prestige Dice Bought:", stats.GetStats().HighestPrestigeBoughtDice.ToString());
	}
}
#endif