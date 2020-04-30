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
				//bat label: '', script: '"C:\\Program Files\\Unity\\Hub\\Editor\\2019.3.3f1\\Editor\\unity.exe" -projectPath "%WORKSPACE%\\Dice Incremental\\" -quit -nographics -batchmode -executeMethod BuildScript.PerformAndroidBuild'
			}
		}
	}
	post {
		always{
			archiveArtifacts 'Dice Incremental/Android_Build/**/*.*'
			
			discordSend description: '**Build:** ${env.BUILD_NUMBER}\\n**Status:** ${currentBuild.currentResult}\\n\\n**Changes:**${changeString}\\n\\n**Artifacts:**\\n- ${env.BUILD_URL}artifact/',
			footer: '',
			image: '',
			link: env.BUILD_URL,
			result: currentBuild.currentResult,
			thumbnail: '',
			title: env.BRANCH_NAME,
			webhookURL: 'https://discordapp.com/api/webhooks/705170565580849192/hrD4Jh-XfK9nPQPrBDuOQil6PvdI7667AolwdN9vNYxQCOiWn7TWDFf7y1Ug1vv0L67q'
			
			cleanWs()
		}
	}
}