using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboMenu : MonoBehaviour
{
	[SerializeField]
	private GameObject m_comboPrefab;

	private List<GameObject> m_comboObjects = new List<GameObject>();

    public void GenerateMenu()
	{
		if (m_comboPrefab == null)
		{
			Debug.LogError("The Combo Menu does not have a valid container prefab!");
		}

		//empty the old list of game objects
		foreach (GameObject gameObject in m_comboObjects)
			Destroy(gameObject);

		m_comboObjects = new List<GameObject>();


		//get the combo manager
		ComboManager c_comboManagerInstance = ComboManager.GetInstance();


		//generate a game object for the different combo's
		foreach(string combo in c_comboManagerInstance.GetUnlockedCombos())
		{
			ComboUIObject c_combo = CreateComboObject();

			c_combo.ConnectCombo(c_comboManagerInstance.GetAllCombos()[combo]);
		}
	}

	private ComboUIObject CreateComboObject()
	{
		//create the new object
		GameObject newComboObject = Instantiate(m_comboPrefab, new Vector3(0, 0, 0), Quaternion.identity);

		//add the object to a list
		m_comboObjects.Add(newComboObject);

		//make this gameobject it's parent
		newComboObject.transform.SetParent(transform);

		//return the new combo
		return newComboObject.GetComponent<ComboUIObject>();
	}
}
