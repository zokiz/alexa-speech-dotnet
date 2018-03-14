@ECHO OFF

REM You can enable these variables on your local machine to aid local development, once 
REM you have added your keys etc do NOT push back to git. To stop git from detecting a
REM change you can stop tracking this file in SourceTree or via bash with this command:
REM     git update-index --assume-unchanged build.cmd
REM and if you want to start tracking again use
REM     git update-index --no-assume-unchanged build.cmd

REM SET nuget_deployments_server=http://localhost:8092
REM SET nuget_deployment_server_api_key=[YOUR_KEY_HERE]
REM SET nuget_server=http://localhost:8091
REM SET nuget_server_api_key=[YOUR_KEY_HERE]
REM SET octopus_server=http://octopusdeploy.local
REM SET octopus_server_api_key=[YOUR_KEY_HERE]
REM Setup the whole environment after initial clone of the repo, only needs to be run ONCE

REM Initialisation that prepares the build environment ready for Cake (or other tools) to operate
IF /I "%1" == "INIT" ( GOTO initialise )
IF /I "%1" == "DEBUG" ( GOTO build_debug )
IF /I "%1" == "RELEASE" ( GOTO build_release )
IF /I "%1" == "TESTS" ( GOTO run_tests )
IF /I "%1" == "PACKAGE" ( GOTO package )
IF /I "%1" == "PUBLISH" ( GOTO publish )
IF /I "%1" == "CREATE_RELEASES" ( GOTO create_releases )

REM All other calls go to the standard runner
GOTO standard_command

:initialise
	
	ECHO Preparing for first run (this only needs to be run once after you clone the repo).
    ECHO Setting build.cmd as git assume unchanged
    CALL git update-index --assume-unchanged build.cmd
	ECHO Setting build.ps1 as git assume unchanged
    CALL git update-index --assume-unchanged build.ps1
	ECHO Setting build.sh as git assume unchanged
    CALL git update-index --assume-unchanged build.sh
    ECHO Setting CommonAssemblyInfo.cs as git assume unchanged
    CALL git update-index --assume-unchanged CommonAssemblyInfo.cs
	CALL powershell .\build.ps1 -target SetupDevelopmentExperience
	ECHO.
	ECHO Development setup completed, environment ready for developement!
	GOTO finished

:build_debug

	IF /I "%2" == "" (
		ECHO Please provide build number with the command in this format 'build debug 1.0.0.0'
	) ELSE (
		CALL powershell .\build.ps1 -target Build -Verbosity Diagnostic -configuration Debug -scriptargs '-build_number="%2"'
	)
	GOTO finished

:build_release

	IF /I "%2" == "" (
		ECHO Please provide build number with the command in this format 'build release 1.0.0.0'
	) ELSE (
		CALL powershell .\build.ps1 -target Build -Verbosity Diagnostic -scriptargs '-build_number="%2"'
	)
	GOTO finished

:run_tests

	CALL powershell .\build.ps1 -target RunTests -Verbosity Diagnostic
	GOTO finished

:package

	IF /I "%2" == "" (
		ECHO Please provide build number with the command in this format 'build package 1.0.0.0'
	) ELSE (
		CALL powershell .\build.ps1 -target BuildPackages -Verbosity Diagnostic -scriptargs '-build_number="%2"'
	)
	GOTO finished

:publish

	IF /I "%2" == "" (
		ECHO Please provide build number with the command in this format 'build publish 1.0.0.0'
	) ELSE (
		CALL powershell .\build.ps1 -target PublishPackages -Verbosity Diagnostic -scriptargs '-build_number="%2"'
	)
	GOTO finished

:create_releases

	IF /I "%2" == "" (
		ECHO Please provide build number with the command in this format 'build create_releases 1.0.0.0'
	) ELSE (
		CALL powershell .\build.ps1 -target CreateReleases -Verbosity Diagnostic -scriptargs '-build_number="%2"'
	)
	GOTO finished

:standard_command

    CALL powershell .\build.ps1 %*

:finished
