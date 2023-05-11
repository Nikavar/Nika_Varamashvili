using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Library.Web.Models
{
    public class UserFormViewModel
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
