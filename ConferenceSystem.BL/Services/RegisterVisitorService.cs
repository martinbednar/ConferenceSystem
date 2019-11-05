
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.UI;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services.UserManagment;
using ConferencySystem.DAL.Data;
using ConferencySystem.DAL.Data.UserIdentity;
using Microsoft.AspNet.Identity;
using PdfSharp;
using PdfSharp.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using TheArtOfDev.HtmlRenderer.Core;
using DbContext = ConferencySystem.DAL.Data.DbContext;

namespace ConferencySystem.BL.Services
{
    public class RegisterVisitorService
    {
        public int AddUser (AppUserDTO userData)
        {
            AppUser user = Mapper.Map<AppUserDTO, AppUser>(userData);

            using (AppUserManager userManager = new AppUserManager(new DbContext()))
            {
                userManager.UserValidator =
                    new UserValidator<AppUser, int>(userManager) {AllowOnlyAlphanumericUserNames = false};
                
                userManager.Create(user, userData.PasswordHash);
                
                AppUser currentUser = userManager.FindByName(user.UserName);

                userManager.AddToRole(currentUser.Id, "visitor");
                
                return currentUser.Id;
            }
        }
    }
}