using System;
using mpp_csharp_stolniceanudenisa_final.model;

namespace mpp_csharp_stolniceanudenisa_final.validators
{
    public class UserValidator //: IValidator<User>
    {
        public void Validate(User user)
        {
            // CheckForNullAndEmptyStrings(user);
        }

        // private void CheckForNullAndEmptyStrings(User user)
        // {
        //     if (String.IsNullOrWhiteSpace(user.Username))
        //     {
        //         throw new ValidationException("Invalid Username");
        //     }
        //
        //     String donorEmailAddress = user.EmailAddress;
        //     if (String.IsNullOrWhiteSpace(donorEmailAddress) || donorEmailAddress.Length < 5 ||
        //         !donorEmailAddress.Contains('@'))
        //     {
        //         throw new ValidationException("Invalid email address");
        //     }
        //
        //     String donorPhoneNumber = user.PhoneNumber;
        //     if (String.IsNullOrWhiteSpace(donorPhoneNumber) || donorPhoneNumber.Length < 7 ||
        //         donorPhoneNumber.Length > 20)
        //     {
        //         throw new ValidationException("Invalid phone number");
        //     }
        }
}