using System.ComponentModel.DataAnnotations;

namespace TaskManager.Data.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public int Prioridad { get; set; }
    }
}