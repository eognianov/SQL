using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using PetClinic.Dto.ExportDto;
using Formatting = Newtonsoft.Json.Formatting;

namespace PetClinic.DataProcessor
{
    using System;

    using PetClinic.Data;

    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var animal = context.Animals.Where(a => a.Passport.OwnerPhoneNumber == phoneNumber).Select(ai =>
                new AnimalDto
                {
                    OwnerName = ai.Passport.OwnerName,
                    AnimalName = ai.Name,
                    Age = ai.Age,
                    SerialNumber = ai.PassportSerialNumber,
                    RegisteredOn = ai.Passport.RegistrationDate.ToString("dd-MM-yyyy")
                }).OrderBy(a => a.Age).ThenBy(a => a.SerialNumber).ToArray();

            var json = JsonConvert.SerializeObject(animal, Formatting.Indented);

            return json;
        }

        public static string ExportAllProcedures(PetClinicContext context)
        {
            var procedure = context.Procedures.OrderBy(p => p.DateTime).Select(p => new ProcedureDto()
            {
                Passport = p.Animal.PassportSerialNumber,
                OwnerNumber = p.Animal.Passport.OwnerPhoneNumber,
                DateTime = p.DateTime.ToString("dd-MM-yyyy"),
                AnimalAids = p.ProcedureAnimalAids.Select(pa => new AnimalAidDto()
                {
                    Name = pa.AnimalAid.Name,
                    Price = pa.AnimalAid.Price
                }).ToArray(),
                TotalPrice = p.ProcedureAnimalAids.Sum(ai => ai.AnimalAid.Price)
            }).OrderBy(p => p.Passport).ToArray();

            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ProcedureDto[]), new XmlRootAttribute("Procedures"));

            serializer.Serialize(new StringWriter(sb), procedure, new XmlSerializerNamespaces(new [] {XmlQualifiedName.Empty, }) );

            return sb.ToString().TrimEnd();
        }
    }
}
