using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsMenu : MonoBehaviour
{
	[SerializeField]
	private GameObject m_statPrefab;

	private List<GameObject> m_statObjects = new List<GameObject>();

	public void GenerateMenu()
	{
		if (m_statPrefab == null)
		{
			Debug.LogError("The Combo Menu does not have a valid container prefab!");
		}

		//empty the old list of game objects
		foreach (GameObject gameObject in m_statObjects)
			Destroy(gameObject);

		m_statObjects = new List<GameObject>();


		//get the stat manager
		SOStatManager c_statManagerInstance = SOStatManager.GetInstance();


		//generate a game object for the different combo's
		foreach(Basestat stat in c_statManagerInstance.GetAllStatObjects())
		{
			StatUIObject c_stat = CreateStatObject();

			c_stat.ConnectStat(stat);
		}
	}

	private StatUIObject CreateStatObject()
	{
		//create the new object
		GameObject newStatObject = Instantiate(m_statPrefab, new Vector3(0, 0, 0), Quaternion.identity);

		//add the object to a list
		m_statObjects.Add(newStatObject);

		//make this gameobject it's parent
		newStatObject.transform.SetParent(transform);

		//return the new combo
		return newStatObject.GetComponent<StatUIObject>();
	}
}
