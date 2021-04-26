using System;
using System.Text;
using System.Linq;
namespace BackpackСode
{
	class BackPack
	{
		static void Main(string[] args)
		{
			//int[] B = new int[] { 1, 50, 100 };
			//int S = 100;
			//double Inf = Math.Pow(10, 6);

			//double[] F= new double[S + 1];

			//for (int i = 1; i < F.Length; i++)			
			//	F[i] = Inf;

			//for (int i = 0; i < F.Length; i++)
			//{
			//	for (int j = 0; j < B.Length; j++)
			//	{
			//		if (i - B[j] >= 0 && F[i - B[j]] < F[i])
			//			F[i] = F[i - B[j]];
			//	}
			//	F[i] += 1;
			//}
			//Console.WriteLine(F[S]);


			int[] B = new int[] { 2, 3, 6, 13, 27, 52 };
			int M = (B.Sum() + 40);
			int num = 1;
			int[] Normalsequence = new int[B.Length];
			while (num == 1) 
			{ 
				try
				{
					Random random = new Random();
					num = random.Next(num, M);
					Console.WriteLine(num);
					num = (int)Delta_revers(num, M);
				}
				catch { Console.WriteLine($"Це число не є взаємно простим {M}"); };
			}
			Console.WriteLine(num);









			string str = Console.ReadLine();
			StringBuilder sb = new StringBuilder();
			foreach (byte b in System.Text.Encoding.Unicode.GetBytes(str))
				sb.Append(Convert.ToString(b, 2).PadLeft(8, '0')).Append(' ');

			string binaryStr = sb.ToString();
			Console.WriteLine(binaryStr);












		}


		static double Delta_revers(double a, double m)
		{

			if (a == 1)
				return 1;
			return (1 - Delta_revers(m % a, a) * m) / a + m;

		}


	}
}
