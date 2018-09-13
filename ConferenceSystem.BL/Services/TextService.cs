using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Mime;
using System.Web;
using AutoMapper;
using ConferencySystem.BL.DTO;
using ConferencySystem.DAL.Data;

namespace ConferencySystem.BL.Services
{
    public class TextService
    {
        public TextDTO GetText(int id)
        {
            using (var db = new DbContext())
            {
                return Mapper.Map<Text, TextDTO>(db.Text.SingleOrDefault(e => e.Id == id));
            }
        }

        public List<TextDTO> GetTexts()
        {
            using (var db = new DbContext())
            {
                return db.Text.Select(t => new TextDTO()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Subject = t.Subject,
                    Content = t.Content
                }).ToList();
            }
        }

        public void SaveTexts(List<TextDTO> emails)
        {
            using (var db = new DbContext())
            {
                foreach (TextDTO email in emails)
                {
                    Text text = Mapper.Map<TextDTO, Text>(email);
                    db.Text.AddOrUpdate(text);
                    db.SaveChanges();
                }
            }
        }
    }
}