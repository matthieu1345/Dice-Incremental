using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


class BuildScript
{
	static void PerformWebglBuild()
	{
		string[] arguments = Environment.GetCommandLineArgs();

		string locationPath = "";
		for (int i = 0; i < arguments.Length; i++)
		{
			if (arguments[i] == "-outputPath")
			{
				locationPath = arguments[i + 1];
			}
		}

		BuildPlayerOptions options = new BuildPlayerOptions();
		options.scenes           = FindEditorScenes();
		options.target           = BuildTarget.WebGL;
		options.locationPathName = locationPath;
		options.options          = BuildOptions.None;

		BuildPipeline.BuildPlayer(options);
	}

	static void PerformAndroidBuild()
	{
		string[] arguments = Environment.GetCommandLineArgs();

		string locationPath = "";
		for (int i = 0; i < arguments.Length; i++)
		{
			if (arguments[i] == "-outputPath")
			{
				locationPath = arguments[i + 1];
			}
		}

		BuildPlayerOptions options = new BuildPlayerOptions();
		options.scenes           = FindEditorScenes();
		options.target           = BuildTarget.Android;
		options.locationPathName = locationPath;
		options.options          = BuildOptions.None;

		BuildPipeline.BuildPlayer(options);
	}

	private static string[ ] FindEditorScenes()
	{
		List<string> editorScenes = new List<string>();
		foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
		{
			if ( !scene.enabled ) continue;
			editorScenes.Add(scene.path);
		}

		return editorScenes.ToArray();
	}
}
