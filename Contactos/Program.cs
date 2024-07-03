using static System.Console;
using Clases;

namespace AppContactos
{
	internal class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				try
				{
					string path = OpModule.GetPath(); //Obtiene el path
					List<Contacto> contactos = OpModule.CargarContactos(path); //Carga Los contactos al iniciar

					int op = 0;
					do 
					{
						Clear();

						//Opciónes del menu
						string[] options =[ 
							"Agregar contacto",
							"Buscar contacto", 
							"Listar contactos",
							"Eliminar contacto", 
							"Guardar contactos",
							"Cargar contactos", 
							"Salir" ];

						IOModule.DisplayMenu("Menú de Contactos:", options); //Imprime el menu principal
						op = IOModule.GetUserIntInput("Opción"); //Obtiene el input del usuario
						switch (op)
						{
							case 1:
								OpModule.AgregarContacto(ref contactos); //Agrega contacto
								break;
							case 2:
								OpModule.BuscarContacto(contactos); //Busca el contacto
								break;
							case 3:
								OpModule.ListarContacto(contactos); //Muestra los contactos
								break;
							case 4:
								OpModule.EliminarContacto(ref contactos); //Elimina el contacto
								break;
							case 5:
								path = OpModule.SelectPath();
								OpModule.GuardarContactos(path, contactos); //Guarda el contacto
								WriteLine($"Contactos guardados en: {path}");
								break;
							case 6:
								path = OpModule.LoadPath();
								contactos = OpModule.CargarContactos(path); //Carga el contacto
								WriteLine($"Contactos cargados de: {path}");
								break;
							case 7:
								WriteLine("Saliendo de la aplicacion");
								break;
							default:
								WriteLine($"{op} No se encuentra en el rango"); //Si coloca un valor fuera del rango creara la exepcion
								break;
						}
						if(op != 1 && op != 2 && op != 4)
						{
							WriteLine("Presione enter para continuar");
							ReadLine();
						}
					} while (op != 7); //Si se selecciona la Opción 7 termina la ejecucion
					
					OpModule.GuardarContactos(OpModule.GetPath(), contactos); //Guardamos al salir
					break; 
				}
				catch (Exception e)
				{
					IOModule.DisplayErrorMessage(e.Message);
				}
			}
		}
	}
}
