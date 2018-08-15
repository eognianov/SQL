using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AutoMapper;
using Newtonsoft.Json;
using PetClinic.Dto.ImportDto;
using PetClinic.Models;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace PetClinic.DataProcessor
{
    using System;

    using PetClinic.Data;

    public class Deserializer
    {
        private const string ERROR_MESSAGE = "Error: Invalid data.";

        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            var deserializedJson = JsonConvert.DeserializeObject<AnimalAidDto[]>(jsonString);
            var sb = new StringBuilder();
            List<AnimalAid> validAnimalAids = new List<AnimalAid>();

            foreach (var animalAidDto in deserializedJson)
            {
                var animalAidExist = validAnimalAids.Any(ai => ai.Name == animalAidDto.Name);
                if (!IsValid(animalAidDto) || animalAidExist)
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                var animalAid = Mapper.Map<AnimalAid>(animalAidDto);
                validAnimalAids.Add(animalAid);
                sb.AppendLine($"Record {animalAidDto.Name} successfully imported.");
            }

            context.AnimalAids.AddRange(validAnimalAids);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            var deserializedJson = JsonConvert.DeserializeObject<AnimalDto[]>(jsonString);
            var sb = new StringBuilder();
            var validAnimals = new List<Animal>();

            foreach (var animalDto in deserializedJson)
            {
                var passportSerialNumberExist =
                    validAnimals.Any(p => p.Passport.SerialNumber == animalDto.Passport.SerialNumber);
                if (!IsValid(animalDto) || !IsValid(animalDto.Passport) || passportSerialNumberExist)
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                var validAnimal = Mapper.Map<Animal>(animalDto);
                validAnimals.Add(validAnimal);
                sb.AppendLine(
                    $"Record {animalDto.Name} Passport №: {animalDto.Passport.SerialNumber} successfully imported.");
            }

            context.Animals.AddRange(validAnimals);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(VetDto[]), new XmlRootAttribute("Vets"));
            var deserialized = (VetDto[])serializer.Deserialize(new StringReader(xmlString));
            var validVets = new List<Vet>();
            var sb = new StringBuilder();

            foreach (var vetDto in deserialized)
            {
                var vetExist = validVets.Any(x => x.PhoneNumber == vetDto.PhoneNumber);
                if (!IsValid(vetDto) || vetExist)
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                var validVet = Mapper.Map<Vet>(vetDto);
                validVets.Add(validVet);
                sb.AppendLine($"Record {vetDto.Name} successfully imported.");
            }

            context.Vets.AddRange(validVets);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ProcedureDto[]), new XmlRootAttribute("Procedures"));
            var deserialized = (ProcedureDto[])serializer.Deserialize(new StringReader(xmlString));

            var validProcedures = new List<Procedure>();
            var sb = new StringBuilder();

            foreach (var procedureDto in deserialized)
            {
                var vetObj = context.Vets.SingleOrDefault(x => x.Name == procedureDto.Vet);
                var animalObj = context.Animals.SingleOrDefault(x => x.PassportSerialNumber == procedureDto.Animal);
                var validProceduresAnimalAids= new List<ProcedureAnimalAid>();
                var allAidsExist = true;

                foreach (var procedureDtoAnimalAid in procedureDto.AnimalAids)
                {
                    var animalAidExist =
                        context.AnimalAids.SingleOrDefault(ai => ai.Name == procedureDtoAnimalAid.Name);

                    if (animalAidExist==null || validProceduresAnimalAids.Any(p=>p.AnimalAid.Name==procedureDtoAnimalAid.Name))
                    {
                        allAidsExist = false;
                        break;
                    }

                    var animalAidProcedure = new ProcedureAnimalAid(){ AnimalAid = animalAidExist};
                    validProceduresAnimalAids.Add(animalAidProcedure);
                }

                if (!IsValid(procedureDto) || vetObj == null || !procedureDto.AnimalAids.All(IsValid) || vetObj == null || animalObj ==null || !allAidsExist)
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                var proc = new Procedure()
                {
                    Animal = animalObj,
                    Vet = vetObj,
                    DateTime = DateTime.ParseExact(procedureDto.DateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    ProcedureAnimalAids = validProceduresAnimalAids
                    
                };

                validProcedures.Add(proc);
                sb.AppendLine("Record successfully imported.");
            }

            context.Procedures.AddRange(validProcedures);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var context = new ValidationContext(obj);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, context, results, true);

            return isValid;
        } 
    }
}
