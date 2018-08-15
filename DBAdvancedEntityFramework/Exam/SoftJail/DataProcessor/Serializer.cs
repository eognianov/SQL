using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoftJail.DataProcessor.ExportDto;

namespace SoftJail.DataProcessor
{

    using Data;
    using System;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisonersToSerialize = new List<PrisonerDto>();

            foreach (var id in ids)
            {
                var prisoner = context.Prisoners.Include(x=>x.Cell).FirstOrDefault(p => p.Id == id);

                var prisiniorDto = new PrisonerDto()
                {
                    Id = prisoner.Id,
                    Name = prisoner.FullName,
                    CellNumber = prisoner.Cell.CellNumber
                    
                };

                List<OfficerDto> prisonerOfficers = new List<OfficerDto>();

                var officers = context.OfficersPrisoners.Where(p => p.PrisonerId == id).Select(op => op.Officer)
                    .ToList();

                foreach (var officer in officers.OrderBy(x=>x.FullName).ThenBy(x=>x.Id))
                {
                    prisonerOfficers.Add(new OfficerDto(){Department = officer.Department.Name, OfficerName = officer.FullName, Salary = officer.Salary});
                    
                }

                prisiniorDto.Officers = prisonerOfficers;
                prisonersToSerialize.Add(prisiniorDto);
            }

            var jsonString = JsonConvert.SerializeObject(prisonersToSerialize.OrderBy(p=>p.Name).ThenBy(p=>p.Id), Newtonsoft.Json.Formatting.Indented);

            return jsonString;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var prisonersToSerialize = new List<PrisonerMailDto>();
            string[] prisoners = prisonersNames.Split(',').ToArray();
            foreach (string name in prisoners)
            {
                var prisoner = context.Prisoners.FirstOrDefault(p=>p.FullName==name);

                var prisonerMails = context.Mails.Where(m => m.Prisoner == prisoner).ToList();
                
                var prisonerMailDtos = new List<MailDto>();

                foreach (var prisonerMail in prisonerMails)
                {
                    prisonerMailDtos.Add(new MailDto(){Description = Reverse( prisonerMail.Description)});
                }

                PrisonerMailDto prisonerDto = new PrisonerMailDto(){Id = prisoner.Id, Name = prisoner.FullName, IncarcerationDate = prisoner.IncarcerationDate.ToString("yyyy-MM-dd"), EncryptedMessages = prisonerMailDtos};
                prisonersToSerialize.Add(prisonerDto);
            }

            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(PrisonerMailDto[]), new XmlRootAttribute("Prisoners"));

            serializer.Serialize(new StringWriter(sb), prisonersToSerialize.OrderBy(p=>p.Name).ThenBy(p=>p.Id).ToArray(), new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty, }));

            return sb.ToString().TrimEnd();


        }

        private static string Reverse(string text)
        {
            if (text == null) return null;

            // this was posted by petebob as well 
            char[] array = text.ToCharArray();
            Array.Reverse(array);
            return new String(array);
        }
    }
}