using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackpackСode
{
	class BackPack
	{
		static Random random = new Random();
		static void Main(string[] args)
		{
		
			

			List<int> Posld = new List<int>();

			
			int count = 13;
			int t = 0;
			for (int i = 1; i < count+1; i++)
			{
				if (i == 1)
					t = Convert.ToInt32(Math.Pow(i, 2) + i + 1);
				else t *= 2;

				Posld.Add(t);
			}


			Console.WriteLine("Послідовність");
			foreach (var item in Posld)
			{
				Console.WriteLine(item);
			}
			int M = (Posld.Sum() + 40);
			int num = 1;
			List<int> Normalsequence = new List<int>();
			while (num == 1)
			{
				try
				{
					
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
			for (int i = 0; i < Posld.Count; i++)
			{
				Normalsequence.Add((int)mod(Posld[i] * num, M));
				Console.Write(Normalsequence[i] + " ");
			}
			Console.WriteLine();
			Console.WriteLine("Ведіть слово котре хочете зашифрувати");
			string str = Console.ReadLine();

			Console.WriteLine();
			int[] codes = new int[str.Length];
			string[] binary_code = StringToBinary(str).Split(" ");

			for (int j = 0; j < codes.Length; j++)
			{
				Console.WriteLine(binary_code[j]);
				
				for (int i = 0; i < binary_code[j].Length; i++)
				{
					
					codes[j] += Convert.ToInt32((binary_code[j][i].ToString())) * Normalsequence[i];
				}
			}

			Console.WriteLine("Шифр послідовність");
			for (int i = 0; i < codes.Length; i++)
			{
				Console.WriteLine(codes[i]);
			}

			Console.WriteLine("Розшифрування");


			StringBuilder sb = new StringBuilder();

			int Nrevers = (int)Delta_revers(num, M);
			for (int k = 0; k < codes.Length; k++)
			{
				List<double> Ans = new List<double>();
				int code = mod(codes[k] * Nrevers, M);
				Ans = getPosld(Posld, code, Ans);
				int h = 0;
				
				foreach (var item in Ans)
				{
					for (int i = h; i < Posld.Count; i++)
					{
						if (item == Posld[i])
						{
							sb.Append("1");
							h = i + 1;			
							break;
							
						}
						else { sb.Append("0");h--; }
					}
								
				}			
				if(k < codes.Length-1)
				sb.Append(" ");			
			}
	
			string result = "";
			foreach (string code in sb.ToString().Split(" "))
			{
				string decode  = code.PadRight(8, '0');
				int translate = Convert.ToInt32(decode,2);
				result += (char)translate;		
			}

			Console.WriteLine(result);

		}



		public static string StringToBinary(string data)
		{
			StringBuilder sb = new StringBuilder();

			foreach (char c in data.ToCharArray())
			{
				sb.Append(Convert.ToString(c, 2).PadLeft(8, '0')).Append(' ');
			}
			return sb.ToString();
		}

		public static string StringToHexidecimal(string data)
		{
			StringBuilder sb = new StringBuilder();

			foreach (char c in data.ToCharArray())
			{
				sb.Append(Convert.ToString(c, 16).PadLeft(8, '0')).Append(' ');
			}
			return sb.ToString();
		}




		static List<double> getPosld(List<int> A, int code, List<double> Ans)
		{

			int S = Convert.ToInt32(code);

			A = A.OrderBy(x => x).ToList();
			int x = -1;

			for (int i = A.Count - 1; i > -1; i--)
			{
				if (S >= A[i] & x < 0 )
				{

					x = S - A[i];
					Ans.Add(A[i]);
				}
				else
				if (x >= A[i] & Ans.Sum() != S)
				{
					x = x - A[i];
					Ans.Add(A[i]);
				}
			}
			return Ans.OrderBy(x => x).ToList();

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
