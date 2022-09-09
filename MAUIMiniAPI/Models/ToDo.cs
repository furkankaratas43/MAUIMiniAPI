using System.ComponentModel.DataAnnotations;

namespace MAUIMiniAPI.Models
{
    public class ToDo
    {
        [Key]
        public int Id { get; set; }
        public string? TodoName { get; set; }
    }
}
