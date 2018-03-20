using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SaveLoad {

	private static List<DiceStats> m_savedDice = new List<DiceStats>();

	private static void SaveDice(BinaryFormatter bf, FileStream file)
	{
		m_savedDice = new List<DiceStats>();
		for ( int i = 0; i < DiceManager.GetInstance().GetDiceList().Count; i++ )
		{
			DiceStats temp = new DiceStats(DiceManager.GetInstance().GetDiceList()[i].GetStats());
			m_savedDice.Add(temp);
		}

		bf.Serialize(file, SaveLoad.m_savedDice);
		file.Close();
	}

	public static void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/savedGame.gd");

		SaveDice(bf, file);

		file.Close();
	}

	public static void Load()
	{
		if ( File.Exists(Application.persistentDataPath + "/savedGame.gd") )
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGame.gd", FileMode.Open);

			m_savedDice = new List<DiceStats>();
			SaveLoad.m_savedDice = (List<DiceStats>)bf.Deserialize(file);

			file.Close();
		}

		LoadDice();
	}

	private static void LoadDice()
	{
		for(int i = 0; i < m_savedDice.Count; i++)
		DiceManager.GetInstance().LoadDice(m_savedDice[i]);
	}
}
