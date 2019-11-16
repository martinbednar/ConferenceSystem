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
using DotVVM.Framework.Controls;

namespace ConferencySystem.BL.Services
{
    public class LectureService
    {
        public void GetProgramsByUser(int userId, GridViewDataSet<LectureDTO> lecturesDataSet, GridViewDataSet<LectureDTO> workshopsDataSet)
        {
            using (var db = new DbContext())
            {
                var userPrograms = db.Users.Find(userId).LecturerInfo.Lectures.Where(lecture => lecture.Active);
                var lectures = userPrograms.Where(lecture => lecture.Type == "přednáška").AsQueryable().ProjectTo<LectureDTO>();
                var workshops = userPrograms.Where(lecture => (lecture.Type == "workshop") || (lecture.Type == "seminář")).AsQueryable().ProjectTo<LectureDTO>();
                lecturesDataSet.LoadFromQueryable(lectures);
                workshopsDataSet.LoadFromQueryable(workshops);
            }
        }

        public void DeactivateProgram(int lectureId)
        {
            using (var db = new DbContext())
            {
                var lecture = db.Lecture.Find(lectureId);
                lecture.Active = false;
                db.SaveChanges();
            }
        }
    }
}