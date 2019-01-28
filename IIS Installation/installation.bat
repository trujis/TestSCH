@echo off
setlocal EnableDelayedExpansion


:: BatchGotAdmin
:-------------------------------------
REM  --> Check for permissions
IF "%PROCESSOR_ARCHITECTURE%" EQU "amd64" (
	>nul 2>&1 "%SYSTEMROOT%\SysWOW64\cacls.exe" "%SYSTEMROOT%\SysWOW64\config\system"
) ELSE (
	>nul 2>&1 "%SYSTEMROOT%\system32\cacls.exe" "%SYSTEMROOT%\system32\config\system"
)

REM --> If error flag set, we do not have admin.
if '%errorlevel%' NEQ '0' (
    echo Requesting administrative privileges...
    goto UACPrompt
) else ( goto gotAdmin )

:UACPrompt
    echo Set UAC = CreateObject^("Shell.Application"^) > "%temp%\getadmin.vbs"
    set params= %*
    echo UAC.ShellExecute "cmd.exe", "/c ""%~s0"" %params:"=""%", "", "runas", 1 >> "%temp%\getadmin.vbs"

    "%temp%\getadmin.vbs"
    del "%temp%\getadmin.vbs"
    exit /B

:gotAdmin
    pushd "%CD%"
    CD /D "%~dp0"
:-------------------------------------- 


REM Thanks to: https://docs.microsoft.com/en-us/iis/application-frameworks/scenario-build-an-aspnet-website-on-iis/configuring-step-1-install-iis-and-asp-net-modules


REM assign arguments to variables
REM IF NOT [%1]==[] SET directory=%1

REM Get parameters from user if they're not specified
REM IF [%1]==[] SET /P directory="Enter name of directory/site: "

SET directory=GuillemSchibstedLover
SET apppool=%directory%Pool
SET webroot=C:\inetpub\wwwroot
SET appcmd=CALL %WINDIR%\system32\inetsrv\appcmd



REM Install IIS features -In some machines, you will need .net 4.7... I don't know how to install it manually (sorry). You can do it in Panel Control.
rem DISM /Online /Enable-Feature /FeatureName:IIS-ApplicationDevelopment /FeatureName:IIS-ASP /FeatureName:IIS-ASPNET /FeatureName:IIS-BasicAuthentication /FeatureName:IIS-CGI /FeatureName:IIS-ClientCertificateMappingAuthentication /FeatureName:IIS-CommonHttpFeatures /FeatureName:IIS-CustomLogging /FeatureName:IIS-DefaultDocument /FeatureName:IIS-DigestAuthentication /FeatureName:IIS-DirectoryBrowsing /FeatureName:IIS-FTPExtensibility /FeatureName:IIS-FTPServer /FeatureName:IIS-FTPSvc /FeatureName:IIS-HealthAndDiagnostics /FeatureName:IIS-HostableWebCore /FeatureName:IIS-HttpCompressionDynamic /FeatureName:IIS-HttpCompressionStatic /FeatureName:IIS-HttpErrors /FeatureName:IIS-HttpLogging /FeatureName:IIS-HttpRedirect /FeatureName:IIS-HttpTracing /FeatureName:IIS-IIS6ManagementCompatibility /FeatureName:IIS-IISCertificateMappingAuthentication /FeatureName:IIS-IPSecurity /FeatureName:IIS-ISAPIExtensions /FeatureName:IIS-ISAPIFilter /FeatureName:IIS-LegacyScripts /FeatureName:IIS-LegacySnapIn /FeatureName:IIS-LoggingLibraries /FeatureName:IIS-ManagementConsole /FeatureName:IIS-ManagementScriptingTools /FeatureName:IIS-ManagementService /FeatureName:IIS-Metabase /FeatureName:IIS-NetFxExtensibility /FeatureName:IIS-ODBCLogging /FeatureName:IIS-Performance /FeatureName:IIS-RequestFiltering /FeatureName:IIS-RequestMonitor /FeatureName:IIS-Security /FeatureName:IIS-ServerSideIncludes /FeatureName:IIS-StaticContent /FeatureName:IIS-URLAuthorization /FeatureName:IIS-WebDAV /FeatureName:IIS-WebServer /FeatureName:IIS-WebServerManagementTools /FeatureName:IIS-WebServerRole /FeatureName:IIS-WindowsAuthentication /FeatureName:IIS-WMICompatibility /FeatureName:WAS-ConfigurationAPI /FeatureName:WAS-NetFxEnvironment /FeatureName:WAS-ProcessModel /FeatureName:WAS-WindowsActivationService /FeatureName:IIS-ASPNET45 /FeatureName:IIS-ASPNET




REM Configuracio AppPool
%appcmd% list apppool "%apppool%" 1>nul
IF "%ERRORLEVEL%" EQU "0" (
	rem icacls "%webroot%" /t /grant "IIS AppPool\%apppool%":(R) 1>nul
) ELSE (
    echo Creando el AppPool
	%appcmd% add apppool /name:%apppool% /managedRuntimeVersion:"v4.0" /managedPipelineMode:"Integrated"	
	%appcmd% set apppool /apppool.name:%apppool% /enable32BitAppOnWin64:true
	%appcmd% set AppPool /apppool.name:%apppool% /managedRuntimeVersion:v4.0
)


REM Damos Permisos al AppPool
icacls "%webroot%" /t /grant "IIS AppPool\%apppool%":(R) 1>nul


REM Creacio del site a IIS
%appcmd% list site /name:"Default Web Site" 1>nul
IF "%ERRORLEVEL%" EQU "0" (
    REM Ya existe
	%appcmd% stop site "Default Web Site"
) ELSE (
    echo Creando el site "Default Web Site"
	%appcmd% add site /name:"Default Web Site" /physicalPath:"%webroot% /bindings:http/*:80:
)


REM Creacion de la Aplicacion en IIS
%appcmd% list app /path:/%directory% 1>nul
IF "%ERRORLEVEL%" EQU "0" (
    REM Ya existe
) ELSE (
    echo Creando la Aplicacion "Default Web Site/%directory%"
	%appcmd% add app /site.name:"Default Web Site" /path:"/%directory%" /physicalPath:"%webroot%\%directory%" /applicationPool:"%apppool%"
)


xcopy /s /y "%~dp0\%directory%" %webroot%\%directory%\

REM Start site
%appcmd% start site "Default Web Site"


REM Interactive mode
if [%1]==[] (
	pause
	exit
)