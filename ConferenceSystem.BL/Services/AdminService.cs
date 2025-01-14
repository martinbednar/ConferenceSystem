﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ConferencySystem.BL.DTO;
using ConferencySystem.BL.Services.UserManagment;
using ConferencySystem.DAL.Data;
using ConferencySystem.DAL.Data.UserIdentity;
using DotVVM.Framework.Controls;
using Microsoft.AspNet.Identity;
using DbContext = ConferencySystem.DAL.Data.DbContext;

namespace ConferencySystem.BL.Services
{
    public class AdminService
    {
        public void GetUsers(GridViewDataSet<AppUserDTO> usersDataSet)
        {
            using (var db = new DbContext())
            {
                var users = db.Users.Where(user => user.Roles.All(role => (role.RoleId == 1) || (role.RoleId == 6))).ProjectTo<AppUserDTO>().ToList();

                int sequencenumber = 1;

                foreach (var userDTO in users)
                {
                    userDTO.SequenceNumber = sequencenumber;
                    sequencenumber++;
                }

                usersDataSet.LoadFromQueryable(users.AsQueryable());
            }
        }

        public void GetVisitors(GridViewDataSet<AppUserDTO> usersDataSet)
        {
            using (var db = new DbContext())
            {
                var users = db.Users.Where(user => user.Roles.All(role => role.RoleId == 5)).ProjectTo<AppUserDTO>().ToList().OrderBy(u => u.RegisterTimestamp);

                int sequencenumber = 1;

                foreach (var userDTO in users)
                {
                    userDTO.SequenceNumber = sequencenumber;
                    sequencenumber++;
                }

                usersDataSet.LoadFromQueryable(users.AsQueryable());
            }
        }

        public void GetLecturers(GridViewDataSet<AppUserDTO> usersDataSet)
        {
            using (var db = new DbContext())
            {
                var users = db.Users.Where(user => user.Roles.All(role => role.RoleId == 4)).ProjectTo<AppUserDTO>().ToList().OrderBy(u => u.RegisterTimestamp);

                int sequencenumber = 1;

                foreach (var userDTO in users)
                {
                    userDTO.SequenceNumber = sequencenumber;
                    sequencenumber++;
                }

                usersDataSet.LoadFromQueryable(users.AsQueryable());
            }
        }

        public List<AppUserDTO> GetLecturers()
        {
            using (var db = new DbContext())
            {
                return db.Users.Where(user => user.Roles.All(role => role.RoleId == 4)).ProjectTo<AppUserDTO>().OrderBy(u => u.RegisterTimestamp).ToList();
            }
        }

        public void DeleteUser(int id)
        {
            using (var db = new DbContext())
            {
                var user = db.Users.Find(id);
                if (user.LecturerInfo != null)
                {
                    db.Lecture.RemoveRange(user.LecturerInfo.Lectures);
                    db.LecturerInfo.Remove(user.LecturerInfo);
                }
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }


        public void DeleteOrganization(int id)
        {
            using (var db = new DbContext())
            {
                var organization = db.Organization.Find(id);
                db.Organization.Remove(organization);
                db.SaveChanges();
            }
        }


        public OrganizationDTO GetOrganization(int organizationId)
        {
            using (var db = new DbContext())
            {
                var organization = Mapper.Map<Organization, OrganizationDTO>(db.Organization.SingleOrDefault(o => o.Id == organizationId));

                return organization;
            }
        }


        public AppUserDTO GetUser(int id)
        {
            using (var db = new DbContext())
            {
                return Mapper.Map<AppUser, AppUserDTO>(db.Users.SingleOrDefault(p => p.Id == id));
            }
        }

        public void SaveUser(AppUserDTO userData)
        {
            using (var db = new DbContext())
            {
                var user = db.Users.Find(userData.Id);

                Mapper.Map(userData, user);

                db.SaveChanges();
            }
        }


        public void SaveOrganization(OrganizationDTO organizationData)
        {
            using (var db = new DbContext())
            {
                var organization = db.Organization.Find(organizationData.Id);
                Mapper.Map(organizationData, organization);

                db.SaveChanges();
            }
        }

        public InvoiceDTO GetInvoice(int userId)
        {
            using (var db = new DbContext())
            {
                return Mapper.Map<Invoice, InvoiceDTO>(db.Invoice.SingleOrDefault(i => i.UserId == userId));
            }
        }

        public LecturerInfoDTO GetLecturerInfo(int lecturerInfoId)
        {
            using (var db = new DbContext())
            {
                return Mapper.Map<LecturerInfo, LecturerInfoDTO>(db.LecturerInfo.SingleOrDefault(i => i.Id == lecturerInfoId));
            }
        }

        public LecturerInfoDTO GetLecturerPhoto(int lecturerInfoId)
        {
            using (var db = new DbContext())
            {
                var lecturerInfo = db.LecturerInfo.Find(lecturerInfoId);

                return new LecturerInfoDTO()
                {
                    PhotoName = lecturerInfo.PhotoName,
                    Photo = lecturerInfo.Photo
                };
            }
        }

        public void changeUserRole(int userId, int oldRoleId, string newRoleName)
        {
            var db = new DbContext();
            var oldRoleName = db.Roles.Find(oldRoleId).Name;

            using (AppUserManager userManager = new AppUserManager(new DbContext()))
            {
                userManager.UserValidator =
                    new UserValidator<AppUser, int>(userManager) { AllowOnlyAlphanumericUserNames = false };

                userManager.RemoveFromRole(userId, oldRoleName);
                userManager.AddToRole(userId, newRoleName);
            }
        }
    }
}