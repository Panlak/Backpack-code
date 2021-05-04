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





			List<int> Posld = new List<int>(){ 2, 3, 6, 13, 27, 52, 66, 87 };
			Console.WriteLine("Послідовність");
			foreach (var item in Posld)
			{
				Console.WriteLine(item);
			}
			int M = (Posld.Sum() + 40);
			int num = 1;
			int[] Normalsequence = new int[Posld.Count];
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

			Console.WriteLine();
			int[] codes = new int[str.Length];
			string[] binary_code = StringToBinary(str).Split(" ");

			for (int j = 0; j < codes.Length; j++)
			{
				Console.WriteLine(binary_code[j]);
				for (int i = 0; i < binary_code[j].Length; i++)
				{
					if (binary_code[j][i].ToString() == "1")
						codes[j] += Normalsequence[i];
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
					for (int i = h; i < Posld.Count; i++)
					{
						if (item == Posld[i])
						{
							Console.Write(@"1");
							h = i + 1;
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




		public static string StringToBinary(string data)
		{
			StringBuilder sb = new StringBuilder();

			foreach (char c in data.ToCharArray())
			{
				sb.Append(Convert.ToString(c, 2).PadLeft(8, '0')).Append(' ');
			}
			return sb.ToString();
		}


		static List<double> getPosld(List<int> A, int code, List<double> Ans)
		{

			int S = Convert.ToInt32(code);
			int index = -1;
			List<int> A_new = new List<int>();
			foreach (var item in A)
			{
				A_new.Add(item);
			}
			int len = A.Count - 1;
			while(Ans.Sum() != S)
			{
				
				 if (S - A_new[len] > 0)
				{
					for ( int j = 0; j < A_new.Count; j++)
					{
						if (A_new[j] + A_new[len]  > S)
						{
							if (A_new[j] < A_new[len])
								A_new.RemoveAt(j);
							else
								A_new.RemoveAt(len);
							break;
						}				
						
						
					}
					if (A_new.Contains(A[len]) & A[len] != Ans.LastOrDefault() & Ans.Sum() + A[len] <= S)
						Ans.Add(A[len]);
					index++;

				}

				len--;
				if (len < 0)
					break;
			}






			//for (int i = A.Length-1; i > -1; i--)
			//{
				
			
			//	if (Ans.Sum() != S)
			//	{
			//		if (S - A[i] > 0)
			//		{

			//			Ans.Add(A[i]);
			//			index++;

			//		}

			//	}
			//	else
			//	{
			//		i++;
			//		Ans.RemoveAt(index);
			//		index--;
				
			//	}
				

			//}
			
			
			return Ans.OrderBy(x => x).ToList();






			//double Inf = Math.Pow(10, 6);

			//double[] F = new double[S + 1];
			//double[] Prev = new double[S + 1];
			//for (int i = 1; i < F.Length; i++)
			//	F[i] = Inf;


			//for (int i = 0; i < Prev.Length; i++)
			//{
			//	Prev[i] = -1;
			//}

			//for (int i = 0; i < F.Length; i++)
			//{
			//	for (int j = 0; j < A.Length; j++)
			//	{
			//		if (i - A[j] >= 0 && F[i - A[j]] < F[i])
			//		{
			//			F[i] = F[i - A[j]];
			//			Prev[i] = A[j];
			//		}
			//	}
			//	F[i] += 1;
			//}

			//int restrv = S;

			//while (restrv > 0)
			//{

			//	Ans.Add(Prev[restrv]);
			//	if (!Ans.Contains(Prev[restrv]))
			//		restrv -= (int)Prev[restrv];
			//	else
			//		restrv -= (int)Prev[restrv];

			//}
			//return Ans.Distinct().ToList();
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
