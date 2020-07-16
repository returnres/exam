using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Freedom
{
    class Program
    {
        static  void Main(string[] args)
        {
            Parallel.For(1, 10, g => Console.WriteLine("g-{0}", g));
        }


        /// <summary>
        /// in questi casi uso async/await + Task.run perchè ho sia I/O bounded che CPU bounded
        /// se non avessi cpu bounded terrei core impegnati nel non fare nulla ma solo aspettare che le operazioni
        /// di I/O download dell immagine siano completate
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        static Task<byte[]> DownloadImage(string url)
        {
            var client = new HttpClient();
            return client.GetByteArrayAsync(url);
        }

        static async Task<byte[]> BlurImage(string imagePath)
        {
            return await Task.Run(() =>
            {
                using (var memoryStream = new MemoryStream())
                {
                    return memoryStream.ToArray();
                }
            });
        }

        static async Task SaveImage(byte[] bytes, string imagePath)
        {
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await fileStream.WriteAsync(bytes, 0, bytes.Length);
            }
        }
    }
}
