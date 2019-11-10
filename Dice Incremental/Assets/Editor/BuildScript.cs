using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


class BuildScript
{
	[MenuItem( "Build/Test")]
	static void PerformWebglBuild()
	{
		BuildPlayerOptions options = new BuildPlayerOptions();
		options.scenes = FindEditorScenes();
		options.locationPathName = "var/lib/jenkins/workspace/Dice-Incremental/docs/";
		options.target = BuildTarget.WebGL;
		options.options = BuildOptions.None;

		BuildPipeline.BuildPlayer( options );
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
