
@echo off

SC CREATE "WA_WorkerServiceNet6" BINPATH= "%~dp0%WorkerServiceNet6.exe"
if not "%ERRORLEVEL%" == "0" goto ERROR1
SC DESCRIPTION "WA_WorkerServiceNet6" "WA Technology" 
echo WA_WorkerServiceNet6 설치완료
echo.

:ERROR1

echo.
echo 설치를 완료했습니다.
pause