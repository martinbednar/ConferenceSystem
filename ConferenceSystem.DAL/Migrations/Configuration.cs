using System;
using System.Collections;
using System.Collections.Generic;
using ConferencySystem.DAL.Data;
using ConferencySystem.DAL.Data.UserIdentity;
using Microsoft.AspNet.Identity;

namespace ConferencySystem.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Data.DbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Id,
                new AppRole(){ Id = 1, Name = "user"},
                new AppRole() { Id = 2, Name = "admin" },
                new AppRole() { Id = 3, Name = "super" }
                );

            context.Users.AddOrUpdate(x => x.Id,
                new AppUser()
                {
                    Id = 1,
                    RegisterTimestamp = DateTime.Now,
                    FirstName = "",
                    LastName = "Uživatel",
                    TitleBefore = "",
                    TitleAfter = "",
                    Email = "uzivatel",
                    UserName = "uzivatel",
                    PasswordHash = "AAvuhHR+UhsEgslJ1FDgtBpfipMZ4IYqY62Bst5BwU2MYbP4SPld7bQgG6ZmENQC7A==",
                    SecurityStamp = "4b11c192-172f-4fb5-af64-2ce367b7ed35",
                    Roles = { new AppUserRole(){RoleId = 1, UserId = 1}}
                },
                new AppUser()
                {
                    Id = 2,
                    RegisterTimestamp = DateTime.Now,
                    FirstName = "",
                    LastName = "Pořadatel",
                    TitleBefore = "",
                    TitleAfter = "",
                    Email = "poradatel",
                    UserName = "poradatel",
                    PasswordHash = "APQllKazNwOG3R+S+mrg/s9hrVsZReQIxp9n1/Rtv8r7XqeTbb6VTLVkkKUfWs+iyA==",
                    SecurityStamp = "4b11c192-172f-4fb5-af64-2ce367b7ed35",
                    Roles = { new AppUserRole() { RoleId = 2, UserId = 2 } }
                },
                new AppUser()
                {
                    Id = 3,
                    RegisterTimestamp = DateTime.Now,
                    FirstName = "",
                    LastName = "Administrátor",
                    TitleBefore = "",
                    TitleAfter = "",
                    Email = "admin",
                    UserName = "admin",
                    PasswordHash = "ALD08pqBjaeKt7k+Er35FpQ1Aj+Hioq194qP2uwRmWmG8APqkc9LEGt5iMIvssbYuA==",
                    SecurityStamp = "4b11c192-172f-4fb5-af64-2ce367b7ed35",
                    Roles = { new AppUserRole() { RoleId = 3, UserId = 3 } }
                }
            );

            context.WorkshopsBlock.AddOrUpdate(x => x.Id,
                new WorkshopsBlock()
                {
                    Id = 1,
                    Name = "WORKSHOP 1",
                    Start = new DateTime(2018,2,19,14,00,00,00),
                    End = new DateTime(2018, 2, 19, 16, 00, 00, 00),
                    Workshops = new List<Workshop>()
                    {
                        new Workshop(){ Id = 1, Name = "Výuka zaměřená na kreativitu", Presenter = "Opher Brayer", Room = "Coworking", Capacity = 20 },
                        new Workshop(){ Id = 2, Name = "Jak se dobře ptát?", Presenter = "Jana Stejskalová, Petra Keprtová", Room = "Seminární sál", Capacity = 20 },
                        new Workshop(){ Id = 3, Name = "Učíme (se) v proinkluzivní škole a vedeme ji", Presenter = "Kamila Zemanová, Barbora Heřmanová", Room = "Kaple", Capacity = 20 },
                        new Workshop(){ Id = 4, Name = "Od zkušenosti k matematice", Presenter = "Anna Antonová", Room = "Klubovna I.", Capacity = 20 },
                        new Workshop(){ Id = 5, Name = "Cizí jazyky napříč předměty neboli CLIL", Presenter = "Petra Vallin", Room = "Nová městská síň", Capacity = 20 },
                        new Workshop(){ Id = 6, Name = "Řízení změny ve škole", Presenter = "Karel Derfl", Room = "Knihovna", Capacity = 20 },
                        new Workshop(){ Id = 7, Name = "\"On si začal...\"", Presenter = "Michal Dubec", Room = "Výstavní sál", Capacity = 20 },
                        new Workshop(){ Id = 8, Name = "Proč a jak využít třídnické hodiny pro podporu vztahů nejen ve třídě", Presenter = "Barbora Pospíšilová", Room = "Klubovna II.", Capacity = 20 },
                    }
                },
                new WorkshopsBlock()
                {
                    Id = 1,
                    Name = "WORKSHOP 2",
                    Start = new DateTime(2018, 2, 20, 8, 30, 00, 00),
                    End = new DateTime(2018, 2, 20, 10, 30, 00, 00),
                    Workshops = new List<Workshop>()
                    {
                        new Workshop(){ Id = 1, Name = "Ochutnávka FIE", Presenter = "Jiří Sehnal, Renata Škeříková", Room = "Coworking", Capacity = 20 },
                        new Workshop(){ Id = 2, Name = "Jak se dobře ptát?", Presenter = "Jana Stejskalová, Petra Keprtová", Room = "Seminární sál", Capacity = 20 },
                        new Workshop(){ Id = 3, Name = "Zažít \"Začít spolu\"", Presenter = "Kamila Zemanová, Barbora Heřmanová", Room = "Kaple", Capacity = 20 },
                        new Workshop(){ Id = 4, Name = "Nástroje rozvoje kultury školy v obrazech", Presenter = "Zdeněk Dlabola", Room = "Klubovna I.", Capacity = 20 },
                        new Workshop(){ Id = 5, Name = "Suportivní a klimatické vyučování v praxi", Presenter = "Robert Čapek", Room = "Nová městská síň", Capacity = 20 },
                        new Workshop(){ Id = 6, Name = "Řízení změny ve škole", Presenter = "Karel Derfl", Room = "Knihovna", Capacity = 20 },
                        new Workshop(){ Id = 7, Name = "Inspiromat pro společné čtení", Presenter = "Dagmar Burdová", Room = "Výstavní sál", Capacity = 20 },
                        new Workshop(){ Id = 8, Name = "Harmonizace kolektivu pomocí komunikačních karet", Presenter = "Lucie Ernestová, Mirka Majerová", Room = "Klubovna II.", Capacity = 20 },
                    }
                },
                new WorkshopsBlock()
                {
                    Id = 1,
                    Name = "WORKSHOP 3",
                    Start = new DateTime(2018, 2, 20, 11, 00, 00, 00),
                    End = new DateTime(2018, 2, 20, 13, 00, 00, 00),
                    Workshops = new List<Workshop>()
                    {
                        new Workshop(){ Id = 1, Name = "Ochutnávka FIE", Presenter = "Jiří Sehnal, Renata Škeříková", Room = "Coworking", Capacity = 20 },
                        new Workshop(){ Id = 2, Name = "Otevíráme dveře čtenářství", Presenter = "Jana Stejskalová, Petra Keprtová", Room = "Seminární sál", Capacity = 20 },
                        new Workshop(){ Id = 3, Name = "Učíme (se) v proinkluzivní škole a vedeme ji", Presenter = "Kamila Zemanová, Barbora Heřmanová", Room = "Kaple", Capacity = 20 },
                        new Workshop(){ Id = 4, Name = "Nástroje rozvoje kultury školy v obrazech", Presenter = "Zdeněk Dlabola", Room = "Klubovna I.", Capacity = 20 },
                        new Workshop(){ Id = 5, Name = "Líný učitel", Presenter = "Robert Čapek", Room = "Robert Čapek", Capacity = 20 },
                        new Workshop(){ Id = 6, Name = "Principy improvizace jako nástroj učení", Presenter = "Tomáš Jireček", Room = "Knihovna", Capacity = 20 },
                        new Workshop(){ Id = 7, Name = "Výuka pro budoucnost KRE4T1VNĚ", Presenter = "Petr Hedbávný", Room = "Výstavní sál", Capacity = 20 },
                        new Workshop(){ Id = 8, Name = "Lidi to tak nenechaj", Presenter = "Jitka Míčková, Martina Čurdová", Room = "Klubovna II.", Capacity = 20 },
                    }
                },
                new WorkshopsBlock()
                {
                    Id = 1,
                    Name = "Přednáška",
                    Start = new DateTime(2018, 2, 19, 16, 30, 00, 00),
                    End = new DateTime(2018, 2, 19, 17, 00, 00, 00),
                    Workshops = new List<Workshop>()
                    {
                        new Workshop(){ Id = 1, Name = "CLIL - inovativní přístup nejen k výuce cizích jazyků", Presenter = "Petra Vallin", Room = "Coworking", Capacity = 80 },
                        new Workshop(){ Id = 2, Name = "Jak budovat podporující prostředí ve škole /role učitele, ředitele v tomto tématu", Presenter = "Karel Derfl", Room = "Seminární sál", Capacity = 60 },
                        new Workshop(){ Id = 3, Name = "Nástroje rozvoje kultury školy v obrazech", Presenter = "Zdeněk Dlabola", Room = "Kaple", Capacity = 50 },
                    }
                }
            );

            context.Cartering.AddOrUpdate(x => x.Id,
                new Cartering()
                {
                    Id = 1,
                    Name = "Kuřecí plátek s rajčaty a mozzarellou, brambory",
                    When = new DateTime(2018, 2, 18, 18, 0, 0, 0),
                    Category = "18. 2. – večeře"
                },
                new Cartering()
                {
                    Id = 2,
                    Name = "Ragů z hlívy ústřičné s koriandrem a smetanou, kváskový chléb",
                    When = new DateTime(2018, 2, 18, 18, 0, 0, 0),
                    Category = "18. 2. – večeře"
                },
                new Cartering()
                {
                    Id = 3,
                    Name = "18. 2. večer – ochutnávka vína",
                    When = new DateTime(2018, 2, 18, 19, 0, 0, 0),
                    Category = "18. 2. večer – ochutnávka vína"
                },
                new Cartering()
                {
                    Id = 4,
                    Name = "Španělský ptáček, rýže",
                    When = new DateTime(2018, 2, 19, 12, 0, 0, 0),
                    Category = "19. 2. – oběd"
                },
                new Cartering()
                {
                    Id = 5,
                    Name = "Zeleninová musaka s balkánským sýrem",
                    When = new DateTime(2018, 2, 19, 12, 0, 0, 0),
                    Category = "19. 2. – oběd"
                },
                new Cartering()
                {
                    Id = 6,
                    Name = "Hovězí vývar se špenátovými knedlíčky",
                    When = new DateTime(2018, 2, 19, 11, 30, 0, 0),
                    Category = "19. 2. – oběd – polévka"
                },
                new Cartering()
                {
                    Id = 7,
                    Name = "19. 2. – večerní raut",
                    When = new DateTime(2018, 2, 19, 20, 0, 0, 0),
                    Category = "19. 2. – večerní raut"
                },
                new Cartering()
                {
                    Id = 8,
                    Name = "19. 2. dopoledne – Coffee break",
                    When = new DateTime(2018, 2, 19, 10, 0, 0, 0),
                    Category = "19. 2. dopoledne – Coffee break"
                },
                new Cartering()
                {
                    Id = 9,
                    Name = "19. 2. odpoledne – Coffee break",
                    When = new DateTime(2018, 2, 19, 15, 0, 0, 0),
                    Category = "19. 2. odpoledne – Coffee break"
                },
                new Cartering()
                {
                    Id = 10,
                    Name = "20. 2. dopoledne – Cofee break",
                    When = new DateTime(2018, 2, 20, 10, 0, 0, 0),
                    Category = "20. 2. dopoledne – Cofee break"
                },
                new Cartering()
                {
                    Id = 11,
                    Name = "18. 2. – večeře – zeleninová polévka",
                    When = new DateTime(2018, 2, 18, 17, 30, 0, 0),
                    Category = "18. 2. – večeře – zeleninová polévka"
                },
                new Cartering()
                {
                    Id = 12,
                    Name = "Zeleninová polévka",
                    When = new DateTime(2018, 2, 19, 11, 30, 0, 0),
                    Category = "19. 2. – oběd – polévka"
                }
            );
        }
    }
}
