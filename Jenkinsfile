pipeline {
  environment {
    imagename = "pmaj3/weight-it"
    registryCredential = 'dockerhubid'
    dockerImage = ''
  }
  agent any
  stages {
    stage('Cloning Git') {
      steps {
        git([url: 'https://github.com/Mayan-Code/weight-it.git', branch: 'main', credentialsId: 'gitid'])
      }
    }
    stage('Test & Building image') {
      steps{
        script {
          dockerImage = docker.build imagename + ":$BUILD_NUMBER" //imagename
        }
      }
    }
    stage('Deploy Image') {
      steps{
        script {
          docker.withRegistry( '', registryCredential ) {
             dockerImage.push("$BUILD_NUMBER")
             dockerImage.push('latest')
          }
        }
      }
    }
    stage('Remove Unused docker image') {
      steps{
         sh "docker rmi $imagename:$BUILD_NUMBER"
         sh "docker rmi $imagename:latest"
      }
    }
  }
}
