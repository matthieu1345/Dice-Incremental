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
				echo 'test'
				//bat label: '', script: '"C:\\Program Files\\Unity\\Hub\\Editor\\2019.3.3f1\\Editor\\unity.exe" -projectPath "%WORKSPACE%\\Dice Incremental\\" -quit -nographics -batchmode -executeMethod BuildScript.PerformAndroidBuild'
			}
		}
	}
	post {
		always{
			//archiveArtifacts 'Dice Incremental/Android_Build/**/*.*'
			
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
	
	discordSend description: '**Build:** ${env.BUILD_NUMBER}\\n**Status:** ${currentBuild.currentResult}\\n\\n**Changes:**${changeString}\\n\\n**Artifacts:**\\n- ${env.BUILD_URL}artifact/',
	footer: '',
	image: '',
	link: env.BUILD_URL,
	result: currentBuild.currentResult,
	thumbnail: '',
	title: env.BRANCH_NAME,
	webhookURL: 'https://discordapp.com/api/webhooks/705170565580849192/hrD4Jh-XfK9nPQPrBDuOQil6PvdI7667AolwdN9vNYxQCOiWn7TWDFf7y1Ug1vv0L67q'
}