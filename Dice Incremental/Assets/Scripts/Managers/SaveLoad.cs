using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SaveLoad {

	private static List<DiceStats> m_savedDice = new List<DiceStats>();

	private static void SaveDice(BinaryFormatter bf, FileStream file)
	{
		for ( int i = 0; i < DiceManager.GetInstance().GetDiceList().Count; i++ )
		{
			DiceStats temp = new DiceStats(DiceManager.GetInstance().GetDiceList()[i].GetStats());
			m_savedDice.Add(temp);
		}

		bf.Serialize(file, SaveLoad.m_savedDice);
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

			SaveLoad.m_savedDice = (List<DiceStats>)bf.Deserialize(file);

			file.Close();
		}
	}
}
