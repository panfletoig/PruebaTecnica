namespace Clases
{
	public class Contacto
	{
		public Guid Id { get; }
		public string Nombre { get; }
		public string Tel { get; }
		public string Email { get; }

        public Contacto(Guid Id, string Nombre, string Tel, string Email)
        {
            this.Id = Id;
			this.Nombre = Nombre;
			this.Tel = Tel;
			this.Email = Email;
        }

		public string GetNombre()
		{
			return this.Nombre;
		} 
		public string GetTel()
		{
			return this.Tel;
		}

        public override string ToString()
		{
			string text = $"Id: {this.Id}\n";
			text += $"Nombre: {this.Nombre}\n";
			text += $"Telefono: {this.Tel}\n";
			text += $"Email: {this.Email}\n";
			return text;
		}
	}
}
