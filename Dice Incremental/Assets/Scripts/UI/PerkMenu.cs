using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkMenu : MonoBehaviour
{
	[SerializeField]
	private GameObject m_perkPrefab;

	private List<GameObject> m_perkObjects = new List<GameObject>();

    public void GenerateMenu()
	{
		if (m_perkPrefab == null)
		{
			Debug.LogError("The Perk Menu does not have a valid container prefab!");
		}

		//empty the old list of game objects
		foreach (GameObject gameObject in m_perkObjects)
			Destroy(gameObject);

		m_perkObjects = new List<GameObject>();


		//get the perk manager
		PerkManager c_perkManagerInstance = PerkManager.GetInstance();


		//generate a game object for the different perk's
		foreach(string perk in c_perkManagerInstance.GetUnlockedPerks())
		{
			PerkUIObject c_perk = CreatePerkObject();

			c_perk.ConnectPerk(c_perkManagerInstance.GetAllPerks()[perk]);
		}
	}

	private PerkUIObject CreatePerkObject()
	{
		//create the new object
		GameObject newPerkObject = Instantiate(m_perkPrefab, new Vector3(0, 0, 0), Quaternion.identity);

		//add the object to a list
		m_perkObjects.Add(newPerkObject);

		//make this gameobject it's parent
		newPerkObject.transform.SetParent(transform);

		//return the new perk
		return newPerkObject.GetComponent<PerkUIObject>();
	}
}
