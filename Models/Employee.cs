using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_de_Empregados.Models
{
    public class Employee
    {
        [Key]
        public int id { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Display(Name = "Nome")]
        public string FullName { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Display(Name = "Setor")]
        public string Sector { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Display(Name = "Salário")]
        public int Salary { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Display(Name = "Vale Alimentação")]
        public int FoodVoucher { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Display(Name = "Vale Transporte")]
        public int TransportationVoucher { get; set; }

    }
}