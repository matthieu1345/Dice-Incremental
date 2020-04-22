using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUIObject : MonoBehaviour
{
	[SerializeField]
	Image IconTop;
	[SerializeField]
	Image IconBottom;

	[SerializeField]
	TextMeshProUGUI statTitle;

	[SerializeField]
	TextMeshProUGUI statDescription;

	[SerializeField]
	TextMeshProUGUI statMax;

	[SerializeField]
	TextMeshProUGUI statCurrent;



	public void ConnectStat(Basestat stat)
	{
		IconTop.sprite = stat.UIIcon;
		IconBottom.sprite = stat.UIIcon;
		statTitle.text = stat.Name;
		statDescription.text = stat.Description;
		statMax.text = "High: " + stat.GetValues().m_maxValue.ToString();
		statCurrent.text = "Current: " + stat.GetValues().m_currentValue.ToString();
	}
}