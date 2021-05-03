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
			Console.WriteLine("Послідовність");
			for (int i = 0; i < Posld.Length; i++)
			{
				Console.Write(Posld[i] + " ");
			}
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

			Console.WriteLine("Надзростаюча послідовність");
			for (int i = 0; i < Normalsequence.Length; i++)
			{
				Normalsequence[i] = (int)mod(Posld[i] * num, M);
				Console.Write(Normalsequence[i] + " ");
			}
			Console.WriteLine();
			Console.WriteLine("Ведіть слово котре хочете зашифрувати");
			string str = Console.ReadLine();
			

			int[] codes = new int[str.Length];
			int index = -1;


		
			Console.WriteLine("-----------------------------------------");
			foreach (byte b in System.Text.Encoding.Unicode.GetBytes(str))
			{
				
				int res = 0;
				string s = Convert.ToString(b, 2).PadLeft(8, '0');
				Console.WriteLine(s);
				for (int i = 0; i < s.Length; i++)
				{
					if (s[i].ToString() == "1")
						res += Normalsequence[i];
				}													
				if (res != 0)
				{
					index++;
					codes[index] = res;
				}			
			}
			Console.WriteLine("Шифр послідовність");
			for (int i = 0; i < codes.Length; i++)
			{
				Console.WriteLine(codes[i]);
			}

			Console.WriteLine("Розшифрування");
			int Nrevers = (int)Delta_revers(num, M);

			for (int k = 0; k < codes.Length; k++)
			{
				List<double> Ans = new List<double>();
				int code = mod(codes[k] * Nrevers, M);
				Console.WriteLine(code);
				Ans = getPosld(Posld, code, Ans);
		
				int h = 0;
				foreach (var item in Ans)
				{				
					for (int i = h; i < Posld.Length; i++)
					{					
						if (item == Posld[i])
						{
							Console.Write(@"1");
							h = i+1;
							break;
						}
						else		
							Console.Write("0");
									
					}
					
				}
				Console.WriteLine();
				
			}
			Console.WriteLine();
			
		}

		static List<double> getPosld(int[] Normalsequence, int code, List<double> Ans)
		{
			int S = Convert.ToInt32(code);
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
				for (int j = 0; j < Normalsequence.Length; j++)
				{
					if (i - Normalsequence[j] >= 0 && F[i - Normalsequence[j]] < F[i])
					{
						F[i] = F[i - Normalsequence[j]];
						Prev[i] = Normalsequence[j];
					}
				}
				F[i] += 1;
			}

			int restrv = S;

			while (restrv > 0)
			{
				Ans.Add(Prev[restrv]);
				restrv -= (int)Prev[restrv];
			}
			return Ans.Distinct().ToList();
		}





		static int mod(int x, int m)
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
