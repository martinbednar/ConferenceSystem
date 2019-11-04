using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ConferencySystem.BL.DTO;
using ConferencySystem.DAL.Data;
using ConferencySystem.DAL.Data.UserIdentity;
using DotVVM.Framework.Controls;
using DbContext = ConferencySystem.DAL.Data.DbContext;

namespace ConferencySystem.BL.Services
{
    public class ProfileService
    {
        public void SaveUser(AppUserDTO userData)
        {
            using (var db = new DbContext())
            {
                var user = db.Users.Find(userData.Id);

                user.WantCert = userData.WantCert;
                user.TitleBefore = userData.TitleBefore;
                user.TitleAfter = userData.TitleAfter;
                user.BirthDate = userData.BirthDate;
                user.BirthPlace = userData.BirthPlace;

                db.SaveChanges();
            }
        }
    }
}