using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Üretim_Planlama_Ödev
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Örnek veri: 5 iş, 3 makine
            int n = 5; // iş sayısı
            int m = 3; // makine sayısı

            int[,] sure = new int[,]
            {
                { 4, 2, 5 },  // İş 1
                { 3, 6, 1 },  // İş 2
                { 7, 4, 3 },  // İş 3
                { 2, 5, 4 },  // İş 4
                { 3, 2, 6 }   // İş 5
            };

            int[,] gecis = new int[,]
            {
                { 0, 3, 2 },  // Makine 1'den diğerlerine geçiş
                { 3, 0, 4 },  // Makine 2'den diğerlerine geçiş
                { 2, 4, 0 }   // Makine 3'ten diğerlerine geçiş
            };

            // DP tablosu ve yol takibi için iki boyutlu diziler
            int[,] dp = new int[n, m];          // Minimum süre tablosu
            int[,] oncekiMakine = new int[n, m]; // Seçilen önceki makineleri tutar

            // İlk iş için sadece süreler
            for (int j = 0; j < m; j++)
            {
                dp[0, j] = sure[0, j];
                oncekiMakine[0, j] = -1; // ilk işte önceki makine yok
            }

            // Dinamik programlama: tüm işleri sırayla hesapla
            for (int i = 1; i < n; i++) // işler
            {
                for (int j = 0; j < m; j++) // şu anki makine
                {
                    dp[i, j] = int.MaxValue;
                    for (int k = 0; k < m; k++) // önceki işin yapıldığı makine
                    {
                        int gecisMaliyeti = gecis[k, j];
                        int toplamSure = dp[i - 1, k] + gecisMaliyeti + sure[i, j];
                        if (toplamSure < dp[i, j])
                        {
                            dp[i, j] = toplamSure;
                            oncekiMakine[i, j] = k;
                        }
                    }
                }
            }

            // Minimum toplam süre ve son işin yapıldığı en iyi makine
            int minSure = int.MaxValue;
            int sonMakine = -1;
            for (int j = 0; j < m; j++)
            {
                if (dp[n - 1, j] < minSure)
                {
                    minSure = dp[n - 1, j];
                    sonMakine = j;
                }
            }

            // Sonuçları yazdır
            Console.WriteLine("Minimum toplam süre: " + minSure);

            // Hangi iş hangi makinede yapılmış? (geri izleme)
            int[] yol = new int[n];
            int makine = sonMakine;
            for (int i = n - 1; i >= 0; i--)
            {
                yol[i] = makine;
                makine = oncekiMakine[i, makine];
            }

            Console.WriteLine("İşlerin makine dağılımı:");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"İş {i + 1} - Makine {yol[i] + 1}");
            }

            Console.ReadLine();
        }
    }
}
