
@echo off

SC CREATE "WA_WorkerServiceNet8" BINPATH= "%~dp0%WorkerServiceNet8.exe"
if not "%ERRORLEVEL%" == "0" goto ERROR1
SC DESCRIPTION "WA_WorkerServiceNet8" "WA Technology" 
echo WA_WorkerServiceNet8 ��ġ�Ϸ�
echo.

:ERROR1

echo.
echo ��ġ�� �Ϸ��߽��ϴ�.
pause