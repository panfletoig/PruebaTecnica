using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace AppContactos
{
	public class IOModule
	{
		//Obtiene el input del user
		public static int GetUserIntInput(string indicator)
		{
			while (true)
			{
				Write($"({indicator})> ");
				if(int.TryParse(ReadLine(), out int value))
				{
					return value;
				}
				WriteLine("Valor no valido");
			}
		}

		//Obtiene el input del user
		public static string GetUserStringInput(string text, string indicator)
		{
			while (true)
			{
				WriteLine(text);
				Write($"({indicator})> ");

				string userInput = ReadLine()!;

				if (!string.IsNullOrEmpty(userInput))
				{
					return userInput;
				}
				Clear();
				WriteLine("No puede estar vacio este campo");
			}
		}

		//Imprime los menus
		public static void DisplayMenu(string title, string[] options)
		{	
			WriteLine(title);
			for(int i = 0; i < options.Length; i++)
			{
				WriteLine($"{i+1}. {options[i]}");
			}
		}

		public static void DisplayErrorMessage(string message)
		{
			Clear();
			WriteLine($"{message} \n");
			ReadLine();
		}
	}
}
