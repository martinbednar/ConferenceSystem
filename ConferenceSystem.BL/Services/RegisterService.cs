using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.UI;
using AutoMapper;
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
    public class RegisterService
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

                userManager.AddToRole(currentUser.Id, "user");

                return currentUser.Id;
            }
        }

        public int AddOrganization(OrganizationDTO organizationData, AppUserDTO userData)
        {
            using (var db = new DbContext())
            {
                Organization organization = Mapper.Map<OrganizationDTO, Organization>(organizationData);
                db.Organization.Add(organization);
                db.SaveChanges();

                int addedUserId = AddUser(userData);

                AppUser addedUser = db.Users.Find(addedUserId);

                organization.Users.Add(addedUser);

                addedUser.VariableSymbol = 2019000 + addedUserId;

                addedUser.InvoiceNumber = addedUser.VariableSymbol.ToString();

                addedUser.Invoice = new Invoice()
                {
                    FileName = "Zálohová faktura-" + addedUser.InvoiceNumber + "-" + addedUser.FirstName + " " + addedUser.LastName + ".pdf",
                    FileBytes = PdfSharpConvert(CreateHtmlInvoice(addedUser, organizationData)),
                    UserId = addedUser.Id
                };

                db.SaveChanges();

                return addedUser.Id;
            }
        }

        public string CreateHtmlInvoice(AppUser user, OrganizationDTO organization)
        {
            string htmlInvoice = @"
            
                <html>
	                <body>
		                <br>
		                <table width=""100%"">
			                <tr>
				                <td width=""60%"" align=""left"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src=""https://konferencnisystem.azurewebsites.net/Content/ZNlogo.png""></td>
				                <td width=""40%"" align=""right"">Zálohová faktura číslo: {InvoiceNumber}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
			                </tr>
		                </table>
		
		                <div style='border:none;border-bottom:solid windowtext 1.0pt;padding:0cm 0cm 1.0pt 0cm'>
			                <p align=""center"" style='text-align:center;border:none;padding:0cm;margin-bottom:0px'><b><span style='font-size:25.0pt;line-height:115%'>Zálohová faktura</span></b></p>
		                </div>
		                <br>
		                <table width=""100%"">
			                <tr>
				                <th width=""50%"" align=""left"">Dodavatel</th>
				                <th width=""50%"" align=""left"">Odběratel</th>
			                </tr>
			                <tr>
				                <td width=""50%"" align=""left"">Zámecké návrší</td>
				                <td width=""50%"" align=""left"">{OrganizationName}</td>
			                </tr>
			                <tr>
				                <td width=""50%"" align=""left"">Jiráskova 133</td>
				                <td width=""50%"" align=""left"">{OrganizationBillStreet}</td>
			                </tr>
			                <tr>
				                <td width=""50%"" align=""left"">570 01  Litomyšl</td>
				                <td width=""50%"" align=""left"">{OrganizationPostalCode} {OrganizationTown}</td>
			                </tr>
			                <tr>
				                <td width=""50%"" align=""left"">IČ: 71294058</td>
				                <td width=""50%"" align=""left"">IČ: {OrganizationIN}</td>
			                </tr>
			                <tr>
				                <td width=""50%"" align=""left"">DIČ: CZ71294058</td>
				                <td width=""50%"" align=""left"">DIČ: {OrganizationVATID}</td>
			                </tr>
			                <tr>
				                <td width=""50%"" align=""left"">&nbsp;</td>
				                <td width=""50%"" align=""left"">&nbsp;</td>
			                </tr>
			                <tr>
				                <td colspan=""2"" width=""100%"" align=""left"">Vyřizuje: Lenka Backová</td>
			                </tr>
			                <tr>
				                <td colspan=""2"" width=""100%"" align=""left"">Telefon: 608 885 826</td>
			                </tr>
		                </table>
		
		                <br>
		                <div style='border:none;border-bottom:solid windowtext 1.0pt;padding:0cm 0cm 1.0pt 0cm'></div>
		                <br>
		
		                <table width=""100%"">
			                <tr>
				                <th width=""100%"" align=""left"">Údaje pro platbu</th>
			                </tr>
			                <tr>
				                <th width=""100%"" align=""left"">&nbsp;</th>
			                </tr>
			                <tr>
				                <td width=""100%"" align=""left"">Číslo účtu: 257996309 / 0300 (Československá obchodní banka)</td>
			                </tr>
			                <tr>
				                <td width=""100%"" align=""left"">Variabilní symbol: {VariableSymbol}</td>
			                </tr>
			                <tr>
				                <td width=""100%"" align=""left"">Celkem k úhradě: 1990,- Kč</td>
			                </tr>
		                </table>
		
		                <br>
		                <div style='border:none;border-bottom:solid windowtext 1.0pt;padding:0cm 0cm 1.0pt 0cm'></div>
		                <br>
		
		                <table width=""100%"">
			                <tr>
				                <td width=""100%"" align=""left"">Datum vystavení: {today}</td>
			                </tr>
			                <tr>
				                <td width=""100%"" align=""left"">Datum splatnosti: {duedate}</td>
			                </tr>
			                <tr>
				                <td width=""100%"" align=""left"">Forma úhrady: bankovním převodem</td>
			                </tr>
			                <tr>
				                <td width=""100%"" align=""left"">&nbsp;</td>
			                </tr>
			                <tr>
				                <td width=""100%"" align=""left"">&nbsp;</td>
			                </tr>
			                <tr>
				                <td width=""100%"" align=""left"">Zálohová faktura není daňový doklad a neobsahuje rozpis DPH.</td>
			                </tr>
			                <tr>
				                <td width=""100%"" align=""left"">Daňový doklad Vám zašleme po úhradě zálohy.</td>
			                </tr>
		                </table>
	                <body>
                </html>
            ";

            htmlInvoice = htmlInvoice.Replace("{InvoiceNumber}", user.InvoiceNumber);
            htmlInvoice = htmlInvoice.Replace("{OrganizationName}", organization.Name);
            htmlInvoice = htmlInvoice.Replace("{OrganizationBillStreet}", organization.BillStreet);
            htmlInvoice = htmlInvoice.Replace("{OrganizationPostalCode}", organization.PostalCode);
            htmlInvoice = htmlInvoice.Replace("{OrganizationTown}", organization.Town);
            htmlInvoice = htmlInvoice.Replace("{OrganizationIN}", organization.IN == 0 ? "" : organization.IN.ToString());
            htmlInvoice = htmlInvoice.Replace("{OrganizationVATID}", organization.VATID);
            htmlInvoice = htmlInvoice.Replace("{VariableSymbol}", user.VariableSymbol.ToString());
            htmlInvoice = htmlInvoice.Replace("{today}", DateTime.Today.ToString("dd.MM.yyyy"));
            htmlInvoice = htmlInvoice.Replace("{duedate}", (DateTime.Today.AddDays(10)).ToString("dd.MM.yyyy"));

            return htmlInvoice;
        }

        public int GetOrganizationId (long IN)
        {
            using (var db = new DbContext())
            {
                OrganizationDTO organization = Mapper.Map<Organization, OrganizationDTO>(db.Organization.SingleOrDefault(p => p.IN == IN));

                if (organization == null)
                {
                    return -1;
                }
                else
                {
                    return organization.Id;
                }
            }
        }

        public AppUserDTO GetUser(int id)
        {
            using (var db = new DbContext())
            {
                return Mapper.Map<AppUser, AppUserDTO>(db.Users.SingleOrDefault(p => p.Id == id));
            }
        }

        public List<AppUserDTO> GetUserEmails()
        {
            using (var db = new DbContext())
            {
                return db.Users.Select(p => new AppUserDTO()
                {
                    Id = p.Id,
                    Email = p.Email,
                    RegisterTimestamp = p.RegisterTimestamp
                }).ToList();
            }
        }

        public byte[] PdfSharpConvert(string html)
        {
            byte[] res;
            using (MemoryStream ms = new MemoryStream())
            {
                PdfSharp.Pdf.PdfDocument pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PageSize.A4);
                pdf.Save(ms);
                res = ms.ToArray();
            }
            return res;
        }
    }
}