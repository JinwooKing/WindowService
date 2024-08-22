using App.WindowsService.Model.Utils;
using System.Reflection;
using WorkerService.Model.Helper;

namespace App.WindowsService.Model.Worker
{
    public class Worker
    {
        private TimeSpan _10Days = new TimeSpan(10, 0, 0, 0);
        private string cycle;
        private Timer timer;

        public Worker(string cycle = "1")
        {
            this.cycle = cycle;
        }

        public async void DoWork()
        {
            try
            {
                while (true)
                {
                    // TODO
                    //ToDo();

                    // Delay
                    await Delay();
                }
            }
            catch (Exception e)
            {
                NlogHelper.LogWrite(e.ToString(), NlogHelper.LogType.Error);
            }
        }

        #region 스케줄 동작 Timer 방식
        public async void Start()
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, GetTimeToNext());

        }
        private async void DoWork(object state)
        {
            try
            {
                // TODO
                // 현재 실행 중인 메서드 정보 가져오기
                MethodBase currentMethod = MethodBase.GetCurrentMethod();
                var timeToNextInterval = GetTimeToNext();
                NlogHelper.LogWrite($"{currentMethod.Name} Running At: {DateTimeOffset.Now}, Cycle: {cycle}, TimeToNextInterval: {timeToNextInterval}, NextTime: {DateTime.Now + timeToNextInterval}");
            }
            catch (Exception e)
            {
                NlogHelper.LogWrite(e.ToString(), NlogHelper.LogType.Error);
            }
        }
        #endregion

        #region 다음 실행 시간 구하는 함수 모음
        /// <summary>
        /// 다음 실행 시간까지 딜레이
        /// </summary>
        /// <returns></returns>
        private async Task Delay()
        {
            // 다음 실행 시간
            var timeToNextInterval = GetTimeToNext();
            NlogHelper.LogWrite($"Running At: {DateTimeOffset.Now}, Cycle: {cycle}, TimeToNextInterval: {timeToNextInterval}, NextTime: {DateTime.Now + timeToNextInterval}");

            // 다음 실행 기간이 Int32.MaxValue 보다 크면 에러(System.ArgumentOutOfRangeException) 발생하므로 10일씩 나누어 Delay
            while (timeToNextInterval > _10Days)
            {
                await Task.Delay(_10Days);
                timeToNextInterval -= _10Days;
                NlogHelper.LogWrite($"Running At: {DateTimeOffset.Now}, Cycle: {cycle}, TimeToNextInterval: {timeToNextInterval}, NextTime: {DateTime.Now + timeToNextInterval}", NlogHelper.LogType.Debug);
            }

            await Task.Delay(timeToNextInterval);
        }

        ///<summary>
        ///다음 실행 시간
        ///</summary>
        ///<returns></returns>
        private TimeSpan GetTimeToNext()
        {
            DateTime now = DateTime.Now;

            if (int.TryParse(cycle, out int intervalInMinutes))
            {
                DateTime nextInterval = now.AddMinutes(intervalInMinutes - now.Minute % intervalInMinutes).AddSeconds(-now.Second + Consts.CYCLETIME_SECOND).AddMilliseconds(-now.Millisecond);
                return nextInterval - now;
            }
            else if (cycle.Equals("DAY"))
            {
                DateTime nextDay = now.AddDays(1);
                DateTime nextDayTargetTime = new DateTime(nextDay.Year, nextDay.Month, nextDay.Day, Consts.CYCLETIME_HOUR, Consts.CYCLETIME_MINUTE, Consts.CYCLETIME_SECOND);
                return nextDayTargetTime - now;
            }
            else if (cycle.Equals("MONTH"))
            {
                DateTime nextMonth = now.AddMonths(1);
                DateTime nextMonthTargetDay = new DateTime(nextMonth.Year, nextMonth.Month, 1, Consts.CYCLETIME_HOUR, Consts.CYCLETIME_MINUTE, Consts.CYCLETIME_SECOND);
                return nextMonthTargetDay - now;
            }
            else if (cycle.Equals("YEAR"))
            {
                DateTime nextYear = now.AddYears(1);
                DateTime nextYearTargetDay = new DateTime(nextYear.Year, 1, 1, Consts.CYCLETIME_HOUR, Consts.CYCLETIME_MINUTE, Consts.CYCLETIME_SECOND);
                return nextYearTargetDay - now;
            }
            else
            {
                return TimeSpan.MaxValue;
            }
        }
        #endregion

    }
}