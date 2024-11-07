using Quartz.Impl;
using Quartz;
using System.Globalization;

namespace ScheduleDemo_1735
{
    internal class Program
    {
        static async Task Main(string[] args) // 非同步
        {
            // 輸入判定
            #region
            // 每 N秒 打印⼀次
            Console.Write("每 N 秒打印一次，請輸入一個正整數：");
            string inputSeconds = Console.ReadLine(); // 將使用者輸入的內容給inputSeconds
            int valueSeconds;

            // 驗證數字，使用 int.TryParse轉換inputSeconds檢查是否為數字
            while (!int.TryParse(inputSeconds, out valueSeconds) || valueSeconds <= 0)
            {
                Console.Write("無效輸入，請重新輸入一個正整數：");
                inputSeconds = Console.ReadLine();

            }
            Console.WriteLine($"【將每{valueSeconds}秒打印一次】");

            // 設定指定時間打印⼀次
            Console.Write("指定時間打印一次，請輸入指定時間(格式HHmmss)：");
            string inputTime = Console.ReadLine();
            DateTime valueTime;

            // 驗證時間格式
            // DateTime.TryParseExact(被解析字串inputTime, 指定格式, 區域設置[註1], 日期時間樣式[註2], 輸出解析後字串valueTime)
            while (!DateTime.TryParseExact(inputTime, "HHmmss", null, DateTimeStyles.None, out valueTime))
            {
                Console.Write("無效輸入，請重新輸入指定時間(格式HHmmss)：");
                inputTime = Console.ReadLine();
            }
            Console.WriteLine($"【將在{valueTime}進行打印】");
            #endregion


            // 創建 Scheduler
            #region
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = await schedulerFactory.GetScheduler();  // 取得Scheduler
            await scheduler.Start(); // 啟用

            // 設定第一個排程（每 N 秒打印一次）
            // 定義Job，使用 JobBuilder 建立作業
            IJobDetail job1 = JobBuilder.Create<PrintPerSeconds>()
                .WithIdentity("job1", "group1")  // 指定識別符，由名稱（job1）和組別（group1）組成
                .Build();

            // 定義Trigger，使用TriggerBuilder建立作業
            ITrigger trigger1 = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1") // 指定識別符
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(valueSeconds)
                    .RepeatForever())
                .Build();
            // WithSimpleSchedule() 方法是用來定義觸發器的簡單執行規則。
            // 配置作業執行的間隔時間(WithIntervalInSeconds)以及重複次數(RepeatForever)。

            // 註冊作業和觸發器
            await scheduler.ScheduleJob(job1, trigger1);

            // 設定第二個排程（在指定時間打印一次）
            IJobDetail job2 = JobBuilder.Create<PrintPerTime>()
                .WithIdentity("job2", "group2")
                .Build();

            ITrigger trigger2 = TriggerBuilder.Create()
                .WithIdentity("trigger2", "group2")
                .StartAt(valueTime)
                .Build();

            await scheduler.ScheduleJob(job2, trigger2);

            Console.WriteLine("排程已設定。按任意鍵退出...");
            Console.ReadKey();
            #endregion
        }
    }

    // 打印訊息的 Job 類別
    public class PrintPerSeconds : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"{DateTime.Now}: 記錄打印。");
            return Task.CompletedTask;
        }
    }
    public class PrintPerTime : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"{DateTime.Now}：紀錄指定時間打印。");
            return Task.CompletedTask;
        }
    }

}


// 註1(區域設置)：IFormatProvider，null：默認使用當前文化
// CultureInfo：指定文化，創建 CultureInfo 物件，並將其作為參數傳入。
//              舉例 var germanCulture = new System.Globalization.CultureInfo("de-DE");

// 註2(日期時間樣式)：
// DateTimeStyles.None：不使用任何額外樣式，通常用這個。