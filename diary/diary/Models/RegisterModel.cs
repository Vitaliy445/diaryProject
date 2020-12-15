using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diary.Models
{
    public class RegisterModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Введите фамилию")]
        public string MiddlName { get; set; }
        [Required(ErrorMessage = "Введите отчество")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Введите почту")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Выберите должность")]
        public int Position_Id { get; set; }
        [Required(ErrorMessage = "Выберите отдел")]
        public int Departament_Id { get; set; }
        [Required(ErrorMessage = "Введите зарплату за час")]
        public double HourlyPayment { get; set; }
        [Required(ErrorMessage = "Введите статус")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}
