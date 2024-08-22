# WorkerService
https://learn.microsoft.com/en-us/dotnet/core/extensions/windows-service?pivots=dotnet-7-0

## package
Microsoft.Extensions.Hosting
Microsoft.Extensions.Hosting.WindowsServices


#### �����ͺ��̽� �α����� �뷮 Ȯ��
 - ��ü ������ ��� ���� ���� Ȯ��  

    exec sp_helpfile

    dbcc shrinkfile([�α����ϸ�], 1000)

#### ���� ������ ����
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