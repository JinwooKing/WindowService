
@echo off

SC CREATE "WA_WorkerServiceNet8" BINPATH= "%~dp0%WorkerServiceNet8.exe"
if not "%ERRORLEVEL%" == "0" goto ERROR1
SC DESCRIPTION "WA_WorkerServiceNet8" "WA Technology" 
echo WA_WorkerServiceNet8 설치완료
echo.

:ERROR1

echo.
echo 설치를 완료했습니다.
pause