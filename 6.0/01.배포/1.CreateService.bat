
@echo off

SC CREATE "WA_WorkerServiceNet6" BINPATH= "%~dp0%WorkerServiceNet6.exe"
if not "%ERRORLEVEL%" == "0" goto ERROR1
SC DESCRIPTION "WA_WorkerServiceNet6" "WA Technology" 
echo WA_WorkerServiceNet6 ��ġ�Ϸ�
echo.

:ERROR1

echo.
echo ��ġ�� �Ϸ��߽��ϴ�.
pause