using System.Linq;

namespace PetClinic.DataProcessor
{
    using System;

    using PetClinic.Data;

    public class Bonus
    {
        public static string UpdateVetProfession(PetClinicContext context, string phoneNumber, string newProfession)
        {
            string result = "";
            var vet = context.Vets.FirstOrDefault(v => v.PhoneNumber == phoneNumber);

            if (vet==null)
            {
                result = $"Vet with phone number {phoneNumber} not found!";
            }
            else
            {
                string oldProfession = vet.Profession;
                vet.Profession = newProfession;
                context.SaveChanges();
                result = $"{vet.Name} profession updated from {oldProfession} to {vet.Profession}.";
            }

            return result;
        }
    }
}
