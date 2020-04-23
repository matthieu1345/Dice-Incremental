using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUIObject : MonoBehaviour
{
	[SerializeField]
	Image backgroundOutline;

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
		backgroundOutline.color = stat.UIChipColor;
		statTitle.text = stat.Name;
		statTitle.color = stat.UITextColor;
		string reverseDescription = Reverse(stat.Description);
		statDescription.text = reverseDescription;
		statDescription.color = stat.UITextColor;
		statMax.text = "High: " + stat.GetValues().m_maxValue.ToString();
		statCurrent.text = "Current: " + stat.GetValues().m_currentValue.ToString();
	}

	public string Reverse(string text)
	{

		char[] cArray = text.ToCharArray();
		string reverse = "";
		string currentPart = "";
		bool isStyle = false;
		for (int i = cArray.Length - 1; i > -1; i--)
		{
			if (!isStyle && cArray[i] == '>')
			{
				isStyle = true;
				reverse = currentPart + reverse;
				currentPart = ">";
			}
			else if (!isStyle && cArray[i] != '>')
			{
				currentPart += cArray[i];
			}
			else if (isStyle && cArray[i] == '<')
			{
				isStyle = false;
				currentPart = cArray[i] + currentPart;
				reverse = currentPart + reverse;
				currentPart = "";
			}
			else if (isStyle && cArray[i] != '<')
			{
				currentPart = cArray[i] + currentPart;
			}
		}

		reverse = currentPart + reverse;

		return reverse;
	}
}