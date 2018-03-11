using UnityEngine;

public static class FunctionLibrary
{
	// ReSharper disable once RedundantAssignment
	public static bool GetComponentChecked<T>(this Component caller,ref T objectRef)
	{
		objectRef = caller.GetComponent<T>();

		return (objectRef != null);
	}


}
