using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FunctionLibrary
{
	public static bool GetComponentChecked<T>(this Component caller,ref T objectRef)
	{
		objectRef = caller.GetComponent<T>();

		return (objectRef != null);
	}


}
