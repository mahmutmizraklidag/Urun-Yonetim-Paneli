using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MMStore.WebUIAPIUsing.Models
{
    public class AdminLoginViewModel
    {
        [ StringLength(30), Required(ErrorMessage = "Kullanıcı adı boş geçilemez!")]
        public string Email { get; set; }
        [Display(Name = "Şifre"), StringLength(30), Required(ErrorMessage = "Şifre alanı boş geçilemez!"), DataType(DataType.Password),MinLength(5,ErrorMessage ="Şifre 5 karekterden az olamaz!")]
        public string Password { get; set; }
    }
}
