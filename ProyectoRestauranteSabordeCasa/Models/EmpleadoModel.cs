using System.ComponentModel.DataAnnotations;


namespace ProyectoRestauranteSabordeCasa.Models
{
    public class EmpleadoModel
    {
        [Display(Name = "Código")] public int IdEmp { get; set; }
        [Display(Name = "Cargo")] public int IdCargo { get; set; }
        [Display(Name = "DNI")] public string? DniEmp { get; set; }
        [Display(Name = "Apellidos")] public string? ApellidoEmp { get; set; }
        [Display(Name = "Nombres")] public string? NombreEmp { get; set; }
        [Display(Name = "Telefono")][DataType(DataType.PhoneNumber)] public string? Telefono { get; set; }
        [Display(Name = "Inicio Contrato")][DataType(DataType.Date)] public DateTime InicioContrato { get; set; }
        [Display(Name = "Sueldo")] public Decimal SueldoEmp { get; set; }
        [Display(Name = "Estado")] public bool Activo { get; set; }

        [Display(Name = "Cargo")] public string? NombreCargo { get; set; }
    }
}
