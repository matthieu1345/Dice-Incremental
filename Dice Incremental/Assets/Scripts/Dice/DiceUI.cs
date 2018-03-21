using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceUI : MonoBehaviour
{
	[SerializeField]
	private int m_collumnCount = 5;

	[SerializeField]
	private GridLayoutGroup m_layoutScript;

	[SerializeField]
	private GameObject m_buyNewDiceObject;

	private readonly List<GameObject> m_diceObjects = new List<GameObject>();

	public int GetDiceCount() { return m_diceObjects.Count;}

	private float m_cachedScaleFactor;
	private CanvasScaler m_cachedCanvasScaler;

	private void Awake()
	{
		if( m_layoutScript != null )
		{
			m_cachedCanvasScaler = GetComponentInParent<CanvasScaler>();
			m_cachedScaleFactor = m_cachedCanvasScaler.scaleFactor;
			SetCellSize();

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

		((RectTransform)transform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ((m_diceObjects.Count / m_collumnCount) + 1) * (m_layoutScript.cellSize.y + m_layoutScript.padding.vertical));

	}


	private void Update()
	{
		if ( Math.Abs(m_cachedCanvasScaler.scaleFactor - m_cachedScaleFactor) > 0 )
		{
			m_cachedScaleFactor = m_cachedCanvasScaler.scaleFactor;
			SetCellSize();
		}
	}

	private void SetCellSize()
	{
		Vector2 cellSize = m_layoutScript.cellSize;
		float fullWidth = 0.75f * m_cachedScaleFactor * m_cachedCanvasScaler.referenceResolution.x;
		float totalSpacing = (m_collumnCount + 1) * m_layoutScript.spacing.x;
		cellSize.x = ( fullWidth - totalSpacing) / m_collumnCount;
		m_layoutScript.cellSize = cellSize;
	}

	public void Reset()
	{
		m_diceObjects.Clear();
	}

}
