using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SaveLoad {

	public class SaveData
	{
		public List<DiceStats> _savedDice = new List<DiceStats>();
		public List<string> _unlockedCombos = new List<string>();
		public StatsData _savedStats = new StatsData();
		public ManaData _savedMana = new ManaData();
	}

	private static SaveData _save = new SaveData();
	private static string _saveLocation = Application.persistentDataPath + "/savedGame.json";

	private static void SaveJson()
	{
		string json = JsonUtility.ToJson(_save, true);

		Debug.Log("Saving as JSON: " + json);

		File.WriteAllText(_saveLocation, json);
	}

	private static void GatherDice()
	{
		_save._savedDice = new List<DiceStats>();
		for ( int i = 0; i < DiceManager.GetInstance().GetDiceList().Count; i++ )
		{
			DiceStats temp = new DiceStats(DiceManager.GetInstance().GetDiceList()[i].GetStats());
			_save._savedDice.Add(temp);
		}
	}

	private static void GatherUnlockedCombos()
	{
		_save._unlockedCombos = ComboManager.GetInstance().GetUnlockedCombos();
	}

	private static void GatherStats()
	{
		_save._savedStats = StatsManager.GetInstance().GetStats();
	}

	private static void GatherMana()
	{
		LevelManager levelManagerInstance = LevelManager.GetInstance();
		_save._savedMana.m_money = levelManagerInstance.Money;
		_save._savedMana.m_xp = levelManagerInstance.Xp;
	}

	public static void Save()
	{
		GatherDice();
		GatherUnlockedCombos();
		GatherStats();
		GatherMana();

		SaveJson();
	}

	public static void Load()
	{
		if ( File.Exists(_saveLocation) )
		{
			_save = JsonUtility.FromJson<SaveData>(File.ReadAllText(_saveLocation));
		}

		LoadDice();
		LoadUnlockedCombos();
		LoadStats();
		LoadMana();
	}

	private static void LoadDice()
	{
		for(int i = 0; i < _save._savedDice.Count; i++)
		DiceManager.GetInstance().LoadDice(_save._savedDice[i]);
	}

	private static void LoadUnlockedCombos()
	{
		ComboManager.GetInstance().LoadUnlockedCombos(_save._unlockedCombos);
	}

	private static void LoadStats()
	{
		StatsManager.GetInstance().LoadStats(_save._savedStats);
	}

	private static void LoadMana()
	{
		LevelManager.GetInstance().LoadMana(_save._savedMana);
	}
}
