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

        public string TranslateText(string text, AppUserDTO dataUser)
        {
            text = text.Replace("{FirstName}", dataUser.FirstName);
            text = text.Replace("{LastName}", dataUser.LastName);
            text = text.Replace("{TitleBefore}", dataUser.TitleBefore);
            text = text.Replace("{TitleAfter}", dataUser.TitleAfter);
            text = text.Replace("{VariableSymbol}", dataUser.VariableSymbol.ToString());
            text = text.Replace("{Email}", dataUser.Email);

            var constans = new ConstantService();
            text = text.Replace("{Price}",constans.GetConstant(3).Value.ToString());

            return text;
        }

        public string TranslateText(string text, UserCompletInfo dataUser)
        {
            text = text.Replace("{FirstName}", dataUser.User.FirstName);
            text = text.Replace("{LastName}", dataUser.User.LastName);
            text = text.Replace("{TitleBefore}", dataUser.User.TitleBefore);
            text = text.Replace("{TitleAfter}", dataUser.User.TitleAfter);
            text = text.Replace("{VariableSymbol}", dataUser.User.VariableSymbol.ToString());
            text = text.Replace("{Email}", dataUser.User.Email);

            text = text.Replace("{HasSundayCoffeeBreak}", (dataUser.Cartering.HasSundayCoffeeBreak ? "ano" : "ne"));
            text = text.Replace("{HasSundaySoup}", (dataUser.Cartering.HasSundaySoup ? "ano" : "ne"));
            text = text.Replace("{HasMondayAMCoffeeBreak}", (dataUser.Cartering.HasMondayAMCoffeeBreak ? "ano" : "ne"));
            text = text.Replace("{HasMondaySoup}", (dataUser.Cartering.HasMondaySoup ? "ano" : "ne"));
            text = text.Replace("{HasMondayPMCoffeeBreak}", (dataUser.Cartering.HasMondayPMCoffeeBreak ? "ano" : "ne"));
            text = text.Replace("{HasMondayRaut}", (dataUser.Cartering.HasMondayRaut ? "ano" : "ne"));
            text = text.Replace("{HasTuesdayCoffeeBreak}", (dataUser.Cartering.HasTuesdayCoffeeBreak ? "ano" : "ne"));
            text = text.Replace("{HasTuesdaySoup}", (dataUser.Cartering.HasTuesdaySoup ? "ano" : "ne"));
            text = text.Replace("{HasTuesdayLunch}", (dataUser.Cartering.HasTuesdayLunch ? "ano" : "ne"));
            text = text.Replace("{SundayDinner}", dataUser.Cartering.SundayDinner);
            text = text.Replace("{MondayLunch}", dataUser.Cartering.MondayLunch);

            text = text.Replace("{Block1}", dataUser.Workshops.Block1);
            text = text.Replace("{EduBreak}", dataUser.Workshops.EduBreak);
            text = text.Replace("{Block2}", dataUser.Workshops.Block2);
            text = text.Replace("{Block3}", dataUser.Workshops.Block3);

            var constans = new ConstantService();
            text = text.Replace("{Price}", constans.GetConstant(3).Value.ToString());

            return text;
        }
    }
}