using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceUI : MonoBehaviour
{
	[SerializeField]
	int m_collumnCount = 5;

	[SerializeField]
	GridLayoutGroup m_layoutScript;

	[SerializeField]
	GameObject m_buyNewDiceObject;

	List<GameObject> m_diceObjects = new List<GameObject>();

	public int GetDiceCount() { return m_diceObjects.Count;}

	void Awake()
	{
		if( m_layoutScript != null )
		{
			Vector2 cellSize = m_layoutScript.cellSize;
			float fullWidth = ( (RectTransform)gameObject.transform ).rect.width * 2;
			float totalSpacing = m_collumnCount * m_layoutScript.spacing.x;
			cellSize.x = ( fullWidth - totalSpacing ) / (m_collumnCount - 1);
			m_layoutScript.cellSize = cellSize;

			m_layoutScript.constraintCount = m_collumnCount;
		}

		if (m_buyNewDiceObject != null)
			m_buyNewDiceObject.transform.SetAsLastSibling();
	}

	public void AddDiceObject(GameObject dice)
	{
		m_diceObjects.Add(dice);

		dice.transform.SetParent(transform);

		if (m_buyNewDiceObject != null)
			m_buyNewDiceObject.transform.SetAsLastSibling();

		m_layoutScript.cellSize = m_layoutScript.cellSize;
	}
}
