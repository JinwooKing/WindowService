
@echo off

SC CREATE "JW_WorkerServiceNet8" BINPATH= "%~dp0%WorkerServiceNet8.exe"
if not "%ERRORLEVEL%" == "0" goto ERROR1
SC DESCRIPTION "JW_WorkerServiceNet8" "JINWOOKING" 
echo JW_WorkerServiceNet8 ��ġ�Ϸ�
echo.

:ERROR1

echo.
echo ��ġ�� �Ϸ��߽��ϴ�.
pause