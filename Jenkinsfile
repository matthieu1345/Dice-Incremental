pipeline {
	options {
		buildDiscarder logRotator(artifactDaysToKeepStr: '', artifactNumToKeepStr: '', daysToKeepStr: '', numToKeepStr: '10')
		disableConcurrentBuilds()
	}
	agent {
		label 'Unity2019212f1'
	}
	environment {
		WEBHOOK = credentials('DiceIncremental-Branches')
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
			
			sendDiscord()
			
			cleanWs()
		}
	}
}



@NonCPS
def sendDiscord(){
	def changeString = ""
	
	def changeSets = currentBuild.changeSets
	for (int i = 0; i < changeSets.size(); i++){
		def entries = changeSets[i].items
		for (int j = 0; j < entries.length; j++){
			def entry = entries[j]
			truncated_ID = entry.commitId.take(7)
			truncated_msg = entry.msg.take(100)
			changeString += "\n- `${truncated_ID}` *${truncated_msg} -  ${entry.author}*"
		}
	}
	
	if (!changeString) {
        changeString = "\n\n - No new changes"
    }
	
	discordSend description: "**Build:** ${env.BUILD_NUMBER}\n**Status:** ${currentBuild.currentResult}\n\n**Changes:**${changeString}\n\n**Artifacts:**\n- ${env.BUILD_URL}artifact/",
	footer: '',
	image: '',
	link: env.BUILD_URL,
	result: currentBuild.currentResult,
	thumbnail: '',
	title: env.BRANCH_NAME,
	webhookURL: env.WEBHOOK
}