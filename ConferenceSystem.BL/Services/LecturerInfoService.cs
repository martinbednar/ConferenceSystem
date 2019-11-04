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
using System.Drawing;
using System.Drawing.Imaging;

namespace ConferencySystem.BL.Services
{
    public class LecturerInfoService
    {
        public LecturerInfoDTO GetLecturerInfo(int userId)
        {
            using (var db = new DbContext())
            {
                var user = db.Users.Find(userId);
                return Mapper.Map<LecturerInfo, LecturerInfoDTO>(db.LecturerInfo.SingleOrDefault(l => l.Id == user.LecturerInfo.Id));
            }
        }

        public int UpdateLecturerInfo(AppUserDTO userData)
        {
            AppUser user = Mapper.Map<AppUserDTO, AppUser>(userData);

            using (AppUserManager userManager = new AppUserManager(new DbContext()))
            {
                userManager.UserValidator =
                    new UserValidator<AppUser, int>(userManager) { AllowOnlyAlphanumericUserNames = false };

                userManager.Create(user, userData.PasswordHash);

                AppUser currentUser = userManager.FindByName(user.UserName);

                userManager.AddToRole(currentUser.Id, "lecturer");

                return currentUser.Id;
            }
        }

        public void SavePhoto(string photoPath, int userId)
        {
            using (var db = new DbContext())
            {
                Image img = Image.FromFile(photoPath);
                MemoryStream tmpStream = new MemoryStream();

                ImageFormat imageFormat = ImageFormat.Jpeg;
                switch (Path.GetExtension(photoPath).ToLower())
                {
                    case ".png":
                        imageFormat = ImageFormat.Png;
                        break;
                    case ".bmp":
                        imageFormat = ImageFormat.Bmp;
                        break;
                    case ".gif":
                        imageFormat = ImageFormat.Gif;
                        break;
                    case ".tiff":
                        imageFormat = ImageFormat.Tiff;
                        break;
                    default:
                        imageFormat = ImageFormat.Jpeg;
                        break;
                }

                img.Save(tmpStream, imageFormat);
                tmpStream.Seek(0, SeekOrigin.Begin);
                byte[] imgBytes = tmpStream.ToArray();

                var user = db.Users.Find(userId);
                user.LecturerInfo.Photo = imgBytes;
                user.LecturerInfo.PhotoName = user.FirstName + " " + user.LastName + "-profilova fotka" + Path.GetExtension(photoPath).ToLower();
                db.SaveChanges();
            }
        }

        public void LoadPhoto(byte[] imgBytes, string photoPath)
        {
            MemoryStream tmpStream = new MemoryStream(imgBytes);
            Image img = Image.FromStream(tmpStream);

            ImageFormat imageFormat = ImageFormat.Jpeg;
            switch (Path.GetExtension(photoPath).ToLower())
            {
                case ".png":
                    imageFormat = ImageFormat.Png;
                    break;
                case ".bmp":
                    imageFormat = ImageFormat.Bmp;
                    break;
                case ".gif":
                    imageFormat = ImageFormat.Gif;
                    break;
                case ".tiff":
                    imageFormat = ImageFormat.Tiff;
                    break;
                default:
                    imageFormat = ImageFormat.Jpeg;
                    break;
            }
            //photoPath = System.IO.Path.GetTempPath();

            //img.Save(System.IO.Path.GetTempPath() + "\\myImage.png", ImageFormat.Png);
            //img.Save(photoPath, imageFormat);
            //img.Save(System.IO.Path.GetTempPath() + photoName, imageFormat);
            //img.Save(tmpStream, imageFormat);
            //using (var fileStream = File.Create(System.IO.Path.GetTempPath() + photoName))
            //{
            //    tmpStream.CopyTo(fileStream);
            //    //CopyStream(tmpStream, fileStream);
            //}
        }
    }
}