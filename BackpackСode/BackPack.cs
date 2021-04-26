using System;
using System.Linq;
using System.Text;
namespace BackpackСode
{
	class BackPack
	{
		static void Main(string[] args)
		{
			int[] B = new int[] { 1, 50, 100 };
			int S = 100;
			double Inf = Math.Pow(10, 6);

			double[] F = new double[S + 1];

			for (int i = 1; i < F.Length; i++)
				F[i] = Inf;

			for (int i = 0; i < F.Length; i++)
			{
				for (int j = 0; j < B.Length; j++)
				{
					if (i - B[j] >= 0 && F[i - B[j]] < F[i])
						F[i] = F[i - B[j]];
				}
				F[i] += 1;
			}
			Console.WriteLine(F[S]);






			int[] Posld = new int[] { 2, 3, 6, 13, 27, 52,66,87 };
			int M = (B.Sum() + 40);
			int num = 1;
			int[] Normalsequence = new int[Posld.Length];
			while (num == 1)
			{
				try
				{
					Random random = new Random();
					num = random.Next(num, M);
					if (IsCoprime(num, M))
					{
						num = (int)Delta_revers(num, M);
					}
					else num = 1;
				}
				catch { Console.WriteLine($"Це число не є взаємно простим {M}"); };
			}
			Console.WriteLine("Mod = " + M);
			Console.WriteLine("Num = " + num);

			for (int i = 0; i < Normalsequence.Length; i++)
			{
				Normalsequence[i] = (int)mod(Posld[i] * num, M);
				Console.Write(Normalsequence[i] + " ");
			}
			Console.WriteLine();
			Console.WriteLine("Ведіть слово котре хочете зашифрувати");
			string str = Console.ReadLine();
			string code = " ";
			foreach (byte b in System.Text.Encoding.Unicode.GetBytes(str))
			{
				int res = 0;	
				string s  = Convert.ToString(b, 2).PadLeft(8, '0');
				for (int i = 0; i < s.Length; i++)								
					if (s[i] == '1')									
						res += Normalsequence[i];

				if (res != 0)
				{
					Console.WriteLine(s);
					Console.WriteLine(res);
					code += res + " ";
				}
			}

			Console.WriteLine(code);
		}
		static double mod(double x, double m)
		{
			return (x % m + m) % m;
		}
		static double Delta_revers(double a, double m)
		{

			if (a == 1)
				return 1;
			return (1 - Delta_revers(m % a, a) * m) / a + m;

		}
		public static bool IsCoprime(double number, double modul)
		{
			if (number == modul)
			{
				return number == 1;
			}
			else
			{
				if (number > modul)
				{
					return IsCoprime(number - modul, modul);
				}
				else
				{
					return IsCoprime(modul - number, number);
				}
			}
		}

	}
}
