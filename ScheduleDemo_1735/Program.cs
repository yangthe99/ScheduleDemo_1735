using Quartz.Impl;
using Quartz;

namespace ScheduleDemo_1735
{
    internal class Program
    {
        static async Task Main(string[] args) // 非同步
        {
            // 用戶輸入間隔時間 N 秒
            int intervalInSeconds;
            while (true)
            {
                Console.WriteLine("請輸入每隔 N 秒執行的間隔時間（秒）：");

                // 使用 TryParse 來檢查是否為有效的數字
                string input = Console.ReadLine();
                if (int.TryParse(input, out intervalInSeconds) && intervalInSeconds > 0)
                {
                    break;  // 解析成功並且數字大於0，則跳出循環
                }
                else
                {
                    Console.WriteLine("無效輸入，請輸入一個正整數。");
                }
            }

            // 用戶輸入指定時間
            DateTime specifiedTime;
            while (true)
            {
                Console.WriteLine("請輸入指定的時間（格式：yyyy-MM-dd HH:mm:ss）：");

                // 使用 TryParse 來檢查是否為有效的數字
                string timeInput = Console.ReadLine();
                if (DateTime.TryParse(timeInput, out specifiedTime))
                {
                    break;  // 解析成功並且數字大於0，則跳出循環
                }
                else
                {
                    Console.WriteLine("無效輸入，請重新輸入時間格式（yyyy-MM-dd HH:mm:ss）！");
                }
            }

            //= DateTime.Parse(Console.ReadLine());
        }
    }
}
