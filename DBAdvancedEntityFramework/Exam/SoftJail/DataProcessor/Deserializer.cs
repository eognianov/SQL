using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using AutoMapper;
using Newtonsoft.Json;
using Remotion.Linq.Clauses.ResultOperators;
using SoftJail.Data.Models;
using SoftJail.Data.Models.Enums;
using SoftJail.DataProcessor.ImportDto;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace SoftJail.DataProcessor
{

    using Data;
    using System;

    public class Deserializer
    {
        private const string ERROR_MESSAGE = "Invalid Data";
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var deserializedDepartments = JsonConvert.DeserializeObject<DepartmentDto[]>(jsonString);

            List<Department> validDepartments =new List<Department>();

            foreach (var departmentDto in deserializedDepartments)
            {
                var validCells = true;

                if (!IsValid(departmentDto))
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                    
                }

                foreach (var cellDto in departmentDto.Cells)
                {
                    if (!IsValid(cellDto))
                    {
                        validCells = false;
                        break;
                    }
                }

                if (!validCells)
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                var department = Mapper.Map<Department>(departmentDto);

                validDepartments.Add(department);
                sb.AppendLine($"Imported {department.Name} with {department.Cells.Count} cells");

            }

            context.Departments.AddRange(validDepartments);
            context.SaveChanges();

            return sb.ToString().TrimEnd();



        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var deserializedPrisoners = JsonConvert.DeserializeObject<PrisonerDto[]>(jsonString);

            List<Prisoner> validPrisoners = new List<Prisoner>();

            foreach (var prisonerDto in deserializedPrisoners)
            {
                var validMails = true;

                if (!IsValid(prisonerDto))
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                foreach (var mailDto in prisonerDto.Mails)
                {
                    if (!IsValid(mailDto))
                    {
                        validMails = false;
                        break;
                    }
                }

                if (!validMails)
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                }


                var prisoner = new Prisoner()
                {
                    FullName = prisonerDto.FullName,
                    Nickname = prisonerDto.Nickname,
                    CellId = prisonerDto.CellId,
                    Age = prisonerDto.Age,
                    IncarcerationDate = DateTime.ParseExact(prisonerDto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Mails = Mapper.Map<Mail[]>(prisonerDto.Mails)

                };

                if (prisonerDto.ReleaseDate != null)
                {
                    prisoner.ReleaseDate = DateTime.ParseExact(prisonerDto.ReleaseDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture);
                }
                //var prisoner = Mapper.Map<Prisoner>(prisonerDto);
                validPrisoners.Add(prisoner);
                sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
            }

            context.Prisoners.AddRange(validPrisoners);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(OfficerDto[]), new XmlRootAttribute("Officers"));
            var deserializedOfficers = (OfficerDto[])serializer.Deserialize(new StringReader(xmlString));
            var validOfficers = new List<Officer>();

            foreach (var officerDto in deserializedOfficers)
            {
                if (!IsValid(officerDto))
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                var position = Enum.TryParse(typeof(Position), officerDto.Position, out object positionResult);
                var weapon = Enum.TryParse(typeof(Weapon), officerDto.Weapon, out object weaponResult);

                if (!weapon)
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                if (!position)
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                    
                }
                var positionAsEnum = Enum.Parse<Position>(officerDto.Position);
                var weaponAsEnum = Enum.Parse<Weapon>(officerDto.Weapon);

                var officer = new Officer()
                {
                    FullName = officerDto.FullName,
                    Salary = officerDto.Salary,
                    Position = positionAsEnum,
                    Weapon = weaponAsEnum,
                    DepartmentId = officerDto.DepartmentId
                };

                var officerPrisons = new List<OfficerPrisoner>();

                foreach (PrisonerOffDto prisonerOffDto in officerDto.Prisoners)
                {
                    OfficerPrisoner offPr = new OfficerPrisoner(){Officer = officer, PrisonerId = prisonerOffDto.Id};
                    officerPrisons.Add(offPr);
                }

                officer.OfficerPrisoners = officerPrisons;
                validOfficers.Add(officer);
                sb.AppendLine($"Imported {officer.FullName} ({officerDto.Prisoners.Length} prisoners)");
            }

            context.Officers.AddRange(validOfficers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }

        private static bool ConvertStrToBool(string boolStrig)
        {
            if (boolStrig == "true")
            {
                return true;
            }

            return false;
        }
    }
}