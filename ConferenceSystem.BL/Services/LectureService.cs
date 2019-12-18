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

        public void GetLectures(GridViewDataSet<LectureDTO> lecturesDataSet)
        {
            using (var db = new DbContext())
            {
                var lectures = db.Lecture.Where(l => l.Active).ProjectTo<LectureDTO>().ToList();

                int sequencenumber = 1;

                foreach (var lectureDTO in lectures)
                {
                    lectureDTO.SequenceNumber = sequencenumber;
                    sequencenumber++;
                    var lecturer = db.Users.Where(u => u.LecturerInfo.Lectures.Contains(db.Lecture.FirstOrDefault(l => l.Id == lectureDTO.Id))).SingleOrDefault();
                    if (lecturer != null)
                    {
                        lectureDTO.LecturerName = lecturer.TitleBefore + " " + lecturer.FirstName + " " + lecturer.LastName + " " + lecturer.TitleAfter;
                        lectureDTO.LecturerId = lecturer.Id;
                    }
                }

                lecturesDataSet.LoadFromQueryable(lectures.AsQueryable());
            }
        }

        public LectureDTO CreateNewLecture(int lecturerId)
        {
            using (var db = new DbContext())
            {
                var newLecture = new Lecture() {
                    Active = false
                };
                db.Users.Find(lecturerId).LecturerInfo.Lectures.Add(newLecture);
                db.SaveChanges();
                return Mapper.Map<Lecture, LectureDTO>(newLecture);
            }
        }

        public LectureDTO GetLecture(int lectureId)
        {
            using (var db = new DbContext())
            {
                return Mapper.Map<Lecture, LectureDTO>(db.Lecture.SingleOrDefault(p => p.Id == lectureId));
            }
        }

        public LectureDTO GetLecturePresentation(int lectureId)
        {
            using (var db = new DbContext())
            {
                var lecture = db.Lecture.Find(lectureId);

                return new LectureDTO()
                {
                    PresentationName = lecture.PresentationName,
                    Presentation = lecture.Presentation
                };
            }
        }

        public LectureDTO GetLectureWorklist(int lectureId)
        {
            using (var db = new DbContext())
            {
                var lecture = db.Lecture.Find(lectureId);

                return new LectureDTO()
                {
                    WorklistName = lecture.WorklistName,
                    Worklist = lecture.Worklist
                };
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

        public void SavePresentation(string filePath, int lectureId)
        {
            using (var db = new DbContext())
            {
                byte[] presentationBytes = File.ReadAllBytes(filePath);

                var lecture = db.Lecture.Find(lectureId);
                lecture.Presentation = presentationBytes;
                lecture.PresentationName = Path.GetFileName(filePath);
                lecture.Active = true;
                db.SaveChanges();
            }
        }

        public void SaveWorklist(string filePath, int lectureId)
        {
            using (var db = new DbContext())
            {
                byte[] worklistBytes = File.ReadAllBytes(filePath);

                var lecture = db.Lecture.Find(lectureId);
                lecture.Worklist = worklistBytes;
                lecture.WorklistName = Path.GetFileName(filePath);
                lecture.Active = true;
                db.SaveChanges();
            }
        }

        public void SaveLecture(LectureDTO dataLecture)
        {
            using (var db = new DbContext())
            {
                dataLecture.Active = true;
                var lecture = db.Lecture.Find(dataLecture.Id);
                Mapper.Map(dataLecture, lecture);
                db.SaveChanges();
            }
        }

        public void DeletePresentation(int lectureId)
        {
            using (var db = new DbContext())
            {
                var lecture = db.Lecture.Find(lectureId);
                lecture.Presentation = null;
                lecture.PresentationName = "";
                db.SaveChanges();
            }
        }

        public void DeleteWorklist(int lectureId)
        {
            using (var db = new DbContext())
            {
                var lecture = db.Lecture.Find(lectureId);
                lecture.Worklist = null;
                lecture.WorklistName = "";
                db.SaveChanges();
            }
        }
    }
}