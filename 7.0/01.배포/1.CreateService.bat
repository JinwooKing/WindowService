
@echo off

SC CREATE "JW_WorkerServiceNet7" BINPATH= "%~dp0%WorkerServiceNet7.exe"
if not "%ERRORLEVEL%" == "0" goto ERROR1
SC DESCRIPTION "JW_WorkerServiceNet7" "JINWOOKING" 
echo JW_WorkerServiceNet7 ��ġ�Ϸ�
echo.

:ERROR1

echo.
echo ��ġ�� �Ϸ��߽��ϴ�.
pause