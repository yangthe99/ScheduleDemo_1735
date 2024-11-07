using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace ScheduleDemo_1735
{
    public class PrintPerTime : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"{DateTime.Now}：紀錄指定時間打印。");
            return Task.CompletedTask;
        }
    }
}
