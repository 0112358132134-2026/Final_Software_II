using System.ComponentModel.DataAnnotations;

namespace SVModel
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        [RegularExpression(@"^[1-9]\d{12}$", ErrorMessage = "Enter a validate DPI")]
        public string Dpi { get; set; } = null!;       
    }
}