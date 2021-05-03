using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace BackpackСode
{
	class BackPack
	{
		static void Main(string[] args)
		{





			int[] Posld = new int[] { 2, 3, 6, 13, 27, 52, 66, 87};
			int M = (Posld.Sum() + 40);
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


			string[] codes = new string[str.Length];
			int index = -1;

			foreach (byte b in System.Text.Encoding.Unicode.GetBytes(str))
			{
				
				int res = 0;	
				string s  = Convert.ToString(b, 2);
				for (int i = 0; i < s.Length; i++)								
					if (s[i] == '1')									
						res += Normalsequence[i];
					
				if (res != 0)
				{
					index++;
					codes[index] = res.ToString();
				}
				
			}

			for (int i = 0; i < codes.Length; i++)
			{
				Console.WriteLine(codes[i]);
			}

			
			for (int k = 0; k < codes.Length; k++)
			{
				
				int S = Convert.ToInt32(codes[k]);
				double Inf = Math.Pow(10, 6);

				double[] F = new double[S + 1];
				double[] Prev = new double[S + 1];
				for (int i = 1; i < F.Length; i++)
					F[i] = Inf;


				for (int i = 0; i < Prev.Length; i++)
				{
					Prev[i] = -1;
				}

				for (int i = 0; i < F.Length; i++)
				{
					for (int j = 0; j < Posld.Length; j++)
					{
						if (i - Posld[j] >= 0 && F[i - Posld[j]] < F[i])
						{
							F[i] = F[i - Posld[j]];
							Prev[i] = Posld[j];
						}
					}
					F[i] += 1;
				}
				
				int restrv  = S;
				
				Console.WriteLine(codes[k]);
				
				List<double> Ans = new List<double>();
				
				
				while (restrv > 0)
				{
					Ans.Add(Prev[restrv]);
					restrv -= (int)Prev[restrv];
				}	
				Ans = Ans.Distinct().ToList();


				foreach (var item in Ans)
				{
				
					for (int i = 0; i < Posld.Length; i++)
					{
						if (item == Posld[i])
							Console.Write("1");
						else
							Console.Write("0");

					}

				}
				Console.WriteLine();
			}

			int Nrevers = (int)Delta_revers(num, M);
			string Result = "";

			for (int i = 0; i < codes.Length; i++)
			{

				Result = mod((Convert.ToInt32(codes[i]) * Nrevers),M) + " ";

				Console.Write(Convert.ToString( Convert.ToInt32(Result),2)+ " ");
			}


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
