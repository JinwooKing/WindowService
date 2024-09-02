
@echo off

SC CREATE "JW_WorkerServiceNet7" BINPATH= "%~dp0%WorkerServiceNet7.exe"
if not "%ERRORLEVEL%" == "0" goto ERROR1
SC DESCRIPTION "JW_WorkerServiceNet7" "JINWOOKING" 
echo JW_WorkerServiceNet7 설치완료
echo.

:ERROR1

echo.
echo 설치를 완료했습니다.
pause