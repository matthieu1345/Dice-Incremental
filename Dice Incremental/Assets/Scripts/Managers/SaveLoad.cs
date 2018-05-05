using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SaveLoad {

	//serialized variables, in order
	private static List<DiceStats> _savedDice = new List<DiceStats>();
	private static List<string> _unlockedCombos = new List<string>();
	private static StatsData _savedStats = new StatsData();

	private static void SaveDice(BinaryFormatter bf, FileStream file)
	{
		_savedDice = new List<DiceStats>();
		for ( int i = 0; i < DiceManager.GetInstance().GetDiceList().Count; i++ )
		{
			DiceStats temp = new DiceStats(DiceManager.GetInstance().GetDiceList()[i].GetStats());
			_savedDice.Add(temp);
		}

		bf.Serialize(file, _savedDice);
	}

	private static void SaveUnlockedCombos(BinaryFormatter bf, FileStream file)
	{
		_unlockedCombos = LevelManager.GetInstance().GetUnlockedCombos();
		
		bf.Serialize(file, _unlockedCombos);
	}

	private static void SaveStats( BinaryFormatter bf, FileStream file )
	{
		_savedStats = StatsManager.GetInstance().GetStats();
		bf.Serialize(file, _savedStats);
	}

	public static void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/savedGame.gd");

		SaveDice(bf, file);
		SaveUnlockedCombos(bf, file);
		SaveStats(bf, file);

		file.Close();
	}

	public static void Load()
	{
		if ( File.Exists(Application.persistentDataPath + "/savedGame.gd") )
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGame.gd", FileMode.Open);

			_savedDice = (List<DiceStats>)bf.Deserialize(file);

			_unlockedCombos = (List<string>)bf.Deserialize(file);

			_savedStats = (StatsData)bf.Deserialize(file);

			file.Close();
		}

		LoadDice();
		LoadUnlockedCombos();
		LoadStats();
	}

	private static void LoadDice()
	{
		for(int i = 0; i < _savedDice.Count; i++)
		DiceManager.GetInstance().LoadDice(_savedDice[i]);
	}

	private static void LoadUnlockedCombos()
	{
		LevelManager.GetInstance().LoadUnlockedCombos(_unlockedCombos);
	}

	private static void LoadStats()
	{
		StatsManager.GetInstance().LoadStats(_savedStats);
	}
}
