using System.ComponentModel.DataAnnotations;

namespace POE.WEB.Models
{
    public class DeployDocumentModel
    {
        [Required]
        public string Adress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}