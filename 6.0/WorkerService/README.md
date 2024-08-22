# WorkerService
https://learn.microsoft.com/en-us/dotnet/core/extensions/windows-service?pivots=dotnet-7-0

## package
Microsoft.Extensions.Hosting
Microsoft.Extensions.Hosting.WindowsServices


#### 데이터베이스 로그파일 용량 확보
 - 전체 공간과 사용 중인 공간 확인  

    exec sp_helpfile

    dbcc shrinkfile([로그파일명], 1000)

#### 과거 데이터 삭제
		DELETE
		FROM AUDIT_DATA
		WHERE occurDt IN (
		SELECT occurDt
		FROM (
			SELECT 	occurDt, 
			ROW_NUMBER() OVER(ORDER BY occurDt asc) AS rnum 
			FROM AUDIT_DATA 
			) ma
		rnum <= 10000
		);		