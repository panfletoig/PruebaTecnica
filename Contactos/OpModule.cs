using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.IO.Enumeration;
using System.Text.Json;
using System.Text.RegularExpressions;
using Clases;
using static System.Console;

namespace AppContactos
{
	public class OpModule
	{
		public static void AgregarContacto(ref List<Contacto> contactos)
		{
			do
			{
				Clear();
				//Obtiene un identificador unico
				Guid id = Guid.NewGuid();

				//Obtiene inputs del user
				string nombre = IOModule.GetUserStringInput("Ingrese el nombre del nuevo contacto:", "Nombre");
				string tel = IOModule.GetUserStringInput("Ingrese el telefono del nuevo contacto:", "Telefono");
				string email = EmailValidator();



				Clear();

				//Si esta seguro se agrega la informacion
				WriteLine("Esta seguro de la informacion ingresada \n1. Si\n2. No");
				if (IOModule.GetUserIntInput("Opción") == 1)
				{
					contactos.Add(new Contacto(id, nombre, tel, email));
				}

				Clear();
				//pregunta si se desea agregar un nuevo contacto
				IOModule.DisplayMenu("Desea agregar un nuevo contacto", ["Si", "No"]);
			} while (IOModule.GetUserIntInput("Opción") == 1);
		}
		private static string EmailValidator()
		{

			while (true)
			{
				string email = IOModule.GetUserStringInput("Ingrese el Email del nuevo contacto:", "Email");
				if (Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
				{
					return email;
				}
				else
				{
					WriteLine("Correo no valido");
				}
			}
		}
		public static void BuscarContacto(List<Contacto> contactos)
		{
			do
			{
				Clear();
				//Imprime el menu
				IOModule.DisplayMenu("Como desea buscar el contacto", ["Nombre", "Numero"]);
				int op = IOModule.GetUserIntInput("Opción");

				Clear();
				switch (op)
				{
					case 1:
						string nombre = IOModule.GetUserStringInput("Ingrese el nombre a buscar:", "Nombre").ToLower();
						Busqueda(x => x.GetNombre().ToLower().Equals(nombre), contactos);
						break;
					case 2:
						string tel = IOModule.GetUserStringInput("Ingrese el telefono a buscar:", "Telefono").ToLower();
						Busqueda(x => x.GetTel().Equals(tel), contactos);
						break;
					default:
						WriteLine($"La opcion seleccionada {op} es invalida");
						break;
				}

				IOModule.DisplayMenu("Desea buscar otro contacto", ["Si", "No"]);
			} while (IOModule.GetUserIntInput("Opción") != 2);
		}
		private static void Busqueda(Func<Contacto, bool> busqueda, List<Contacto> contactos)
		{
			Clear();
			//Variable que indica si existe el objeto a buscar
			bool found = false;

			//Recorre todo el array hasta ver que uno coincida y lo imprime
			for (int i = 0; i<contactos.Count; i++)
			{
				if (busqueda(contactos[i]))
				{
					WriteLine(contactos[i]);
					found = true;
				}
			}
			if (!found)
			{
				WriteLine("Contacto no encontrado");
			}
		}
		public static void ListarContacto(List<Contacto> contactos)
		{
			Clear();
			//Imprime la lista de contactos
			for (int i = 0; i < contactos.Count; i++)
			{
				WriteLine($"{i+1}:");
				WriteLine(contactos[i]);
            }
		}
		public static void EliminarContacto(ref List<Contacto> contactos)
		{
			do
			{
				Clear();
				//Imprime el menu
				IOModule.DisplayMenu("Como desea buscar el contacto", ["Nombre", "Numero"]);
				int op = IOModule.GetUserIntInput("Opción");

				Clear();
				switch (op)
				{
					case 1:
						string nombre = IOModule.GetUserStringInput("Ingrese el nombre a buscar:", "Nombre").ToLower();
						Delete(x => x.GetNombre().ToLower().Equals(nombre), ref contactos);
						break;
					case 2:
						string tel = IOModule.GetUserStringInput("Ingrese el telefono a buscar:", "Telefono").ToLower();
						Delete(x => x.GetTel().Equals(tel), ref contactos);
						break;
					default:
						WriteLine($"La opcion seleccionada {op} es invalida");
						break;
				}

				IOModule.DisplayMenu("Desea Eliminar otro contacto", ["Si", "No"]);
			} while (IOModule.GetUserIntInput("Opción") != 2);
		}
		private static void Delete(Func<Contacto, bool> busqueda, ref List<Contacto> contactos)
		{
			Clear();
			bool found = false;
			//Elimina la coincidencia
			for (int i = 0; i < contactos.Count; i++)
			{
				if (busqueda(contactos[i]))
				{
					contactos.Remove(contactos[i]);
					found = true;
				}
			}
			if (!found)
			{
				WriteLine("Contacto no encontrado");
			}
		}
		public static void GuardarContactos(string path, List<Contacto> contactos)
		{
			try
			{
				JsonSerializerOptions options = new() { WriteIndented = true };
				string text = JsonSerializer.Serialize(contactos, options); //serializa

				File.WriteAllText(path, text); //Escribe el archivo
			}
			catch (Exception e)
			{
				IOModule.DisplayErrorMessage(e.Message);
			}
		}
		public static List<Contacto> CargarContactos(string path)
		{
			List<Contacto> contactos = [];

			try
			{
				string text = File.ReadAllText(path); //Obtenemos el texto de constactos.json
			
				//Si no es nulo o vacio deserializa
				if (!string.IsNullOrWhiteSpace(text)) { 
					contactos = JsonSerializer.Deserialize<List<Contacto>>(text)!;
				}

			}
			catch (Exception e)
			{
				IOModule.DisplayErrorMessage(e.Message);
			}
			return contactos;
		}
		public static string GetPath()
		{
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Json\Contactos.json");
			CreatePath(path);
			return path;
		}
		public static string SelectPath()
		{
			Clear();
			string path = GetPath(); //Obtiene el path predeterminado
		
			//Pregunta si quiere usar otro path
			IOModule.DisplayMenu("Deseas usar una ruta personalizado", ["Si", "No"]);
			int op = IOModule.GetUserIntInput("Opción");
			Clear();

			//Si es si le pide que ingrese el directorio y nombre del archivo
            if (op == 1)
			{
				string directory = IOModule.GetUserStringInput("Ingrese la ruta", "Directorio");
				directory += "\\";
				string fileName = IOModule.GetUserStringInput("Ingrese el nombre del archivo (ej. Contactos)", "Nombre");
				fileName += ".json"; 

				string NewPath = Path.Combine(directory, fileName);
				Clear();

				//Pregunta si esta seguro y si es asi crea el path y el archivo
				IOModule.DisplayMenu("Esta seguro de la informacion ingresada?", ["Si", "No"]);
				if(IOModule.GetUserIntInput("Opción") == 1)
				{
					CreatePath(NewPath);
					path = NewPath;
				}
			}
            return path;
		}
		
		private static void CreatePath(string path)
		{
			string DirectoryPath = path;
			for(int i = 0; i < DirectoryPath.Length; i++)
			{
				if (DirectoryPath[DirectoryPath.Length - 1 - i] == '\\') 
				{
					DirectoryPath = DirectoryPath[..(DirectoryPath.Length - i - 1)]; //Substring hasta cortar el json
					break;
				}
			}
			//Ve si existe la carpeta o sino la crea
			if (!Directory.Exists(DirectoryPath))
			{
				Directory.CreateDirectory(DirectoryPath);
			}

			//Ve si existe el archivo o lo crea
			if (!File.Exists(path))
			{
				//Si no existe crea el path
				FileStream fs = File.Create(path);
				fs.Close();
			}
		}
		public static string LoadPath()
		{
			Clear();
			IOModule.DisplayMenu("Desea cargar el path predeterminado", ["Si", "No"]);
			string path = GetPath();

			if(IOModule.GetUserIntInput("Opción") == 2)
			{
				path = IOModule.GetUserStringInput("Ingrese el path (formato .json)", "Path");
			}

			return path;
		}
	}
}
