using System.ComponentModel.DataAnnotations;

namespace ProjectAPI.BusinessLogicLayer.DTOs.OrderDTOs
{
    public class OrderRequestDTO
    {
        [Required(ErrorMessage = "Basket ID is required")]
        public string BasketID { get; set; }

        [Required(ErrorMessage = "User email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "User phone is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string UserPhone { get; set; }

        [Required(ErrorMessage = "User address is required")]
        public string UserAddress { get; set; }
    }
}