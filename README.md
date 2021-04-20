
# SmartDev SonarQube Setup Document

## General Information

* SmartDev SonarQube Host: https://sd-sonarqube.smartdev.vn
* We are using Github as the source code repository, for another git service such as Bitbucket or Gitlab, The diffrient things is the way we execute the CI Script such as GH Actions, BB Pipelines or Gitlab CI

### Secrets

* `SONARQUBE_HOST` SonarQube host
* `SONARQUBE_TOKEN` SonarQube login token

## .NET Core Project

* Builder: MSBuild and `dotnet` CLI tool
* Setup GH Action script

```
name: SonaQube-dotNet

on: push
jobs:
  sonarQubeTrigger:
    name: SonarQube dotNet Trigger
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@master
    - uses: actions/setup-java@v2
      with:
        distribution: 'adopt' # See 'Supported distributions' for available options
        java-version: '14'
    - uses: actions/setup-dotnet@v1
      with:
        dotnet-version: '5.0.x' # SDK Version to use
    - run: dotnet new tool-manifest
    - run: dotnet tool install dotnet-sonarscanner
      working-directory: ./CSharp/TestCSharpSonarQube
    - run: dotnet sonarscanner begin /k:"SonarQubeSetup" /d:sonar.host.url="${{secrets.SONARQUBE_HOST}}" /d:sonar.login="${{secrets.SONARQUBE_TOKEN}}" /n:SonarQube-Demo-dotNet
      working-directory: ./CSharp/TestCSharpSonarQube
    - run: dotnet build TestCSharpSonarQube.sln
      working-directory: ./CSharp/TestCSharpSonarQube
    - run: dotnet sonarscanner end /d:sonar.login="${{secrets.SONARQUBE_TOKEN}}"
      working-directory: ./CSharp/TestCSharpSonarQube
```

* Setup Java (for SonarQube CLI)
* Setup .NET Core
* Create .NET Core tools manifest
* Install `dotnet-sonarscanner` using `dotnet` CLI
* Active and build (trigger SonarQube)
* Publish SonarQube result to SQ server

## Java Project

* Source base: Spring Boot with Gradle build tool
* Build tool: Gradle

```
name: SonaQube-Java

on: push
jobs:
  sonarQubeTrigger:
    name: SonarQube Java Trigger
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@master
    - uses: actions/setup-java@v2
      with:
        distribution: 'adopt' # See 'Supported distributions' for available options
        java-version: '14'
    - run: ./gradlew sonarqube -Dsonar.host.url=${{secrets.SONARQUBE_HOST}} -Dsonar.login=${{secrets.SONARQUBE_TOKEN}} -Dsonar.projectName=SonarQube-Demo-Java
      working-directory: ./Java/sonarqube-demo/
```

## Android Project

```
name: SonaQube-Android

on: push
jobs:
  sonarQubeTrigger:
    name: SonarQube Android Trigger
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@master
    - uses: actions/setup-java@v2
      with:
        distribution: 'adopt' # See 'Supported distributions' for available options
        java-version: '14'
    - name: Setup Android SDK
      uses: android-actions/setup-android@v2
    - run: ./gradlew sonarqube -Dsonar.host.url=${{secrets.SONARQUBE_HOST}} -Dsonar.login=${{secrets.SONARQUBE_TOKEN}} -Dsonar.projectName=SonarQube-Demo-Android
      working-directory: ./Android/SonarQubeDemoAndroid

```

## PHP Project

```
name: SonaQube-PHP

on: push
jobs:
  sonarQubeTrigger:
    name: SonarQube PHP Trigger
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@master
    - name: Setup PHP Action
      uses: shivammathur/setup-php@2.10.0
    - uses: actions/setup-java@v2
      with:
        distribution: 'adopt' # See 'Supported distributions' for available options
        java-version: '14'
    - run: composer require rogervila/php-sonarqube-scanner
    - run: composer require rogervila/php-sonarqube-scanner && ./vendor/bin/sonar-scanner -Dsonar.host.url=${{secrets.SONARQUBE_HOST}} -Dsonar.login=${{secrets.SONARQUBE_TOKEN}} -Dsonar.projectName=SonarQube-Demo-PHP
      working-directory: ./PHP/sonarqube-demo-php
    
```

