using System;
using System.ComponentModel.DataAnnotations;

namespace Repos
{
	class Program
	{
		static string[] wordsTranslateBefore20 = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", 
													"ten", "elve", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

		static string[] wordsTranslateTens = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };	


		//269 - max count


		static void Main(string[] args)
		{
			StartProgramm();
        }

		static void StartProgramm()
		{
			Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(new StreamReader("Actions.txt").ReadToEnd() + "\n");

            Console.Write("\n\n" + "Set number of action: ");
			string? numberAction = Console.ReadLine();

			switch (numberAction)
			{
				case "0":
					{
						Console.WriteLine("\n\n" + "Exit..." + "\n\n");
						return;
					}

				case "1":
					{
						try
						{
							StartCheck();
						}
						catch
						{
                            Console.WriteLine("\n" + "Data is not correct!" + "\n\n");
                        }

						StartProgramm();
						return;
					}

				case "2":
					{
						Console.WriteLine(new StreamReader("Rules.txt").ReadToEnd());

						StartProgramm();

						return;
					}

				default:
					{
						Console.Clear();

						StartProgramm();

						return;
					}
			}
		}
		static void StartCheck()
		{
            Console.Write("Set count of word (Max = 269): ");
			int countWord = Convert.ToInt32(Console.ReadLine());

			if (countWord > 269) throw new Exception();

			int correctAnswer = 0;

			var words = GenerateDictionaryNumbers(countWord);

			foreach (var word in words)
			{
                Console.Write(word.Key + " -> translate: ");
				string? translate = Console.ReadLine();

				if (word.Value == translate)
				{
                    Console.WriteLine("\n" + "Correct!" + "\n");
					correctAnswer++;
                }

				if (word.Value != translate)
				{
					Console.WriteLine("\n" + "FAIL!" + "\n");
				}
			}

            Console.WriteLine("\n" + $"Correct {correctAnswer} in {words.Count}.");
        }


		static Dictionary<long, string> GenerateDictionaryNumbers(int countNumber)
		{
			Dictionary<long, string> numbers = new();

			GetNumbersByRange(ref numbers, (int) Math.Round(countNumber * 0.10), 1000000000000);
			GetNumbersByRange(ref numbers, (int)Math.Round(countNumber * 0.15), 1000000000);
			GetNumbersByRange(ref numbers, (int)Math.Round(countNumber * 0.2), 1000000);
			GetNumbersByRange(ref numbers, (int)Math.Round(countNumber * 0.2), 1000);
			GetNumbersByRange(ref numbers, countNumber - numbers.Count, 100);

            return numbers;
		}
		static void GetNumbersByRange(ref Dictionary<long, string> numbers, int countNumber, long range)
		{
			for (int i = 0; i < countNumber; i++)
			{
				long number = new Random().NextInt64(0, range);
				string word = GetWordByNumber(number);

				if (numbers.ContainsKey(number))
				{
					GetNumbersByRange(ref numbers, countNumber - i, range);
					return;
				}

				numbers.Add(number, word);
			}
		}
		static string GetWordByNumber(long number)
		{
            string numberStr = ConvertStringToCorrectForm(number.ToString());
			string result = "";
			int rank = 1;

			while (numberStr.Length > 0)
			{
				string temp = numberStr.Substring(0, 3);
				numberStr = numberStr.Substring(3);

				if (temp != "000")
				{
					if (temp[0] != '0')
					{
                        result += wordsTranslateBefore20[Convert.ToInt32(temp[0].ToString()) - 1] + " hundred and ";
					}

					if (temp.Substring(1) != "00")
					{
						int numberTemp = Convert.ToInt32(temp.Substring(1));

						if (numberTemp < 20)
						{
							result += wordsTranslateBefore20[Convert.ToInt32(temp.Substring(1)) - 1];
						}

						if (numberTemp >= 20)
						{
                            result += wordsTranslateTens[Convert.ToInt32(temp[1].ToString()) - 2];

							if (Convert.ToInt32(temp[2].ToString()) - 1 > -1)
							{
								result += "-" + wordsTranslateBefore20[Convert.ToInt32(temp[2].ToString()) - 1];
							}
						}
					}

					switch (rank)
					{
						case 1:
							{
								result += " billion ";
								break;
							}

						case 2:
							{
								result += " million ";
								break;
							}

						case 3:
							{
								result += " thousand ";
								break;
							}


					}
				}

				rank++;
			}

			return result;
		}
		static string ConvertStringToCorrectForm(string numberStr)
		{
			bool isCorrect = true;

			while (isCorrect)
			{
				if (numberStr.Length == 12)
				{
					isCorrect = false;
					continue;
				}

				numberStr = '0' + numberStr;
			}

			return numberStr;
		}
	}

}