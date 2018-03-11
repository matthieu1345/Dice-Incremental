﻿using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class FunctionLibrary
{
	// ReSharper disable once RedundantAssignment
	public static bool GetComponentChecked<T>(this Component caller,ref T objectRef)
	{
		objectRef = caller.GetComponent<T>();

		return (objectRef != null);
	}

	public static T[] GetAllInstances<T>() where T : ScriptableObject
	{
		string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name); //FindAssets uses tags check documentation for more info
		T[] a = new T[guids.Length];
		for (int i = 0; i < guids.Length; i++) //probably could get optimized 
		{
			string path = AssetDatabase.GUIDToAssetPath(guids[i]);
			a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
		}

		return a;

	}

	public static Type[] GetAllDerivedTypes(this AppDomain aAppDomain, Type aType)
	{
		List<Type> result = new List<Type>();
		Assembly[] assemblies = aAppDomain.GetAssemblies();
		foreach (Assembly assembly in assemblies)
		{
			Type[] types = assembly.GetTypes();
			foreach (Type type in types)
			{
				if (type.IsSubclassOf(aType))
					result.Add(type);
			}
		}
		return result.ToArray();
	}
}