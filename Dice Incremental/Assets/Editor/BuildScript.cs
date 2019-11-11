using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


class BuildScript
{
	static void PerformWebglBuild()
	{
		BuildPlayerOptions options = new BuildPlayerOptions();
		options.scenes = FindEditorScenes();
		options.target = BuildTarget.WebGL;
		options.locationPathName = "d:/JenkinsSlave/workspace/Dice-Incremental/Dice-Incremental-WebGL/docs/";
		options.options = BuildOptions.None;

		BuildPipeline.BuildPlayer( options );
	}

	static void PerformAndroidBuild()
	{
		BuildPlayerOptions options = new BuildPlayerOptions();
		options.scenes           = FindEditorScenes();
		options.target           = BuildTarget.Android;
		options.locationPathName = "d:/JenkinsSlave/workspace/Dice-Incremental/Dice-Incremental-Android/android_build/";
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
