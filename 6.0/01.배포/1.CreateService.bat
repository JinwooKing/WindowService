
@echo off

SC CREATE "JW_WorkerServiceNet6" BINPATH= "%~dp0%WorkerServiceNet6.exe"
if not "%ERRORLEVEL%" == "0" goto ERROR1
SC DESCRIPTION "JW_WorkerServiceNet6" "JinwooKing" 
echo JW_WorkerServiceNet6 ��ġ�Ϸ�
echo.

:ERROR1

echo.
echo ��ġ�� �Ϸ��߽��ϴ�.
pause