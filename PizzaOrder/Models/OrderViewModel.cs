using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Order.Persistence;
namespace PizzaOrder.Models
{
    public class OrderViewModel
    {
        public List<Tuple<Menu, int>> Food; //A megjelenítendő termékek
        public int FailNumber;

        public int FullPrice;
        [Required(ErrorMessage = "A név megadása kötelező!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A cím megadása kötelező!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "A telefonszám megadása kötelező!")]
        [Phone(ErrorMessage = "A telefonszám formátuma nem megfelelő.")]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "A telefonszám formátuma, vagy hossza nem megfelelő.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
