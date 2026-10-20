using GymManagementDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagementBLL.ViewModels.MemberViewModels
{
    internal class MemberToUpdateViewModel
    {
        public string Name { get; set; } = null!;

        public string? Photo { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(100, MinimumLength = 7, ErrorMessage = "Email must be between 7 and 100 letters")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]

        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Name is required.")]
        [Phone(ErrorMessage = "Invalid Phone number")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone number must be valid Egyptian Phone number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Name is required.")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Building Number is required.")]
        [Range(1, 9000, ErrorMessage = "Building Number must be between 1 and 9000")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street must be between 2 and 30 letters")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "Street is required.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City must be between 2 and 30 letters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Ciry can contain only letters and spaces")]
        public string City { get; set; } = null!;
    }
}
