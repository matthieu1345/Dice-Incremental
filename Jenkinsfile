pipeline {
	options {
		buildDiscarder logRotator(artifactDaysToKeepStr: '', artifactNumToKeepStr: '', daysToKeepStr: '', numToKeepStr: '10')
		disableConcurrentBuilds()
	}
	agent {
		label 'Unity2019212f1'
	}
	
	stages{
		stage('scm'){
			steps{
			checkout scm
			}
		}
		stage('build'){
			steps{
				bat label: '', script: '"C:\\Program Files\\Unity\\Hub\\Editor\\2019.3.3f1\\Editor\\unity.exe" -projectPath "%WORKSPACE%\\Dice Incremental\\" -quit -nographics -batchmode -executeMethod BuildScript.PerformAndroidBuild'
			}
		}
	}
	post {
		always{
			archiveArtifacts 'Dice Incremental/Android_Build/**/*.*'
			cleanWs()
		}
	}
}