using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstancedMonoBehaviour<T> : MonoBehaviour 
	where T : InstancedMonoBehaviour<T>, new()
{
	[ReadOnly]
	public static T m_instance = null;


	//call base to set instance
	protected virtual void Awake()
	{
		if (m_instance == null)
			m_instance = (T)this;
		else if (m_instance != this)
			Destroy(this);
	}

	public static T GetInstance()
	{
		if ( m_instance == null )
			m_instance = FindObjectOfType<T>();

		return m_instance;
	}

}
