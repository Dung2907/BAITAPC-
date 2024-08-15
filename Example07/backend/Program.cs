using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Security.Cryptography;

namespace Example07
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Sinh khóa và in ra
            GenerateAndDisplayKey();

            // Khởi động ứng dụng ASP.NET Core
            CreateHostBuilder(args).Build().Run();
        }

        public static void GenerateAndDisplayKey()
        {
            // Tạo khóa 32 byte (256 bit)
            byte[] key = new byte[32];

            // Sử dụng RNGCryptoServiceProvider để tạo khóa ngẫu nhiên
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }

            // Chuyển đổi khóa thành chuỗi base64
            string base64Key = Convert.ToBase64String(key);

            // In khóa ra màn hình
            Console.WriteLine("Generated Key (Base64):");
            Console.WriteLine(base64Key);

            // In chiều dài của khóa để xác nhận kích thước
            Console.WriteLine($"Key Length (Bytes): {key.Length}");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    // Đọc cấu hình từ appsettings.json
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:5115"); // Cấu hình URL và cổng
                });
    }
}
