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
            CommandTimeout = 1000;
        }

        protected override void Seed(Data.DbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Id,
                new AppRole(){ Id = 1, Name = "user"},
                new AppRole() { Id = 2, Name = "admin" },
                new AppRole() { Id = 3, Name = "super" }
                );

            context.Organization.AddOrUpdate(x => x.Id,
                new Organization()
                {
                    Id = 1,
                    BillStreet = "Ulice 123",
                    IN = 123456789,
                    Name = "Moje organizace",
                    PostalCode = "100 00",
                    Town = "Město",
                    VATID = "",
                    Users = new List<AppUser>()
                    {
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
                            Roles = { new AppUserRole() { RoleId = 1, UserId = 1 } }
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
                    }
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

            context.Constant.AddOrUpdate(x => x.Id,
                new Constant() { Id = 1, Name = "Počet účastníků", Value = 170 },
                new Constant() { Id = 2, Name = "Počet dnů na splacení faktury", Value = 15 },
                new Constant() { Id = 3, Name = "Cena na zálohové faktuře", Value = 2300 }
            );

            context.Text.AddOrUpdate(x => x.Id,
                new Text() { Id = 1, Name = "Email po registraci", Subject = "Nakopněte svoji školu - potvrzení registrace", Content = "<p>Vaše přihláška na festival Nakopněte svoji školu byla přijata.</p><p><br data-cke-filler=\"true\"></p><p>Vážíme si energie, kterou chcete věnovat svému vzdělávání. Je to skvělá cesta ke zlepšování každé školy. Děkujeme.</p><p>Místo na konferenci Vám závazně potvrdíme po přijetí platby na náš účet. Zálohovou fakturu najdete v příloze. Pokud platbu neobdržíme do 30.11., bude vaše registrace stornována.</p><p><br data-cke-filler=\"true\"></p><p>Přijetí platby vám potvrdíme na e-mail, který jste uvedli při registraci: {Email}</p><p><br data-cke-filler=\"true\"></p><p>Jméno účastníka: {FirstName} {LastName}</p><p>Údaje k provedení platby</p><p>Číslo účtu pro platbu: 257996309 / 0300</p><p>Variabilní symbol: {VariableSymbol}</p><p>Částka: 2 300,- Kč</p><p><br data-cke-filler=\"true\"></p><p>Hromadné platby za více osob nejsou z organizačních důvodu možné.</p><p>S dotazy, které se týkají platby, se můžete obracet na Lenku Backovou: <a href=\"mailto:lenka.backova@zamecke-navrsi.cz\">lenka.backova@zamecke-navrsi.cz</a>, 608 885 826</p><p><br data-cke-filler=\"true\"></p><p>Registrace na jednotlivých workshopy a výběr jídel bude probíhat přes náš registrační systém: <a href=\"http://konferencnisystem.azurewebsites.net/\">http://konferencnisystem.azurewebsites.net</a></p><p>K přihlášení používejte e-mail a heslo, které jste zadali při registraci.</p><p><br data-cke-filler=\"true\"></p><p><br data-cke-filler=\"true\"></p><p>Některá jména osobností, které potkáte v roce 2019 na festivalu, již visí na našem webu. &nbsp;Přesný program ještě ladíme a bude brzy zveřejněn.</p><p>Sledujte: <a href=\"http://www.nakopnetesvojiskolu.cz\">www.nakopnetesvojiskolu.cz</a></p><p>nebo Facebook: nakopnete.svoji.skolu</p><p>Nezapomeňte si také včas rezervovat ubytování. Více informací najdete taktéž na webu.</p><p><br data-cke-filler=\"true\"></p><p>Těšíme se na Vás v Litomyšli.</p><p><br data-cke-filler=\"true\"></p><p>Na tento e-mail neodpovídejte. Jedná se o automatickou odpověď. V případě potřeby využijte kontakty na našem webu: <a href=\"http://www.nakopnetesvojiskolu.cz\">www.nakopnetesvojiskolu.cz</a></p>" },
                new Text() { Id = 2, Name = "Email po registraci - naplněná kapacita", Subject = "Nakopněte svoji školu - potvrzení registrace", Content = "<p>Vážíme si energie, kterou chcete věnovat svému vzdělání. Je to skvělá cesta pro lepší školy. Děkujeme.</p><p>Aktuálně již byla naplněna kapacita festivalu Nakopněte svoji školu. Byl/a jste zaregistrován/a jako náhradník/náhradnice. Může se však stát, že některý z přihlášených nezašle včas platbu nebo se nakonec nebude moci zúčastnit. V tomto případě bychom se Vám ozvali a zjistili zda Váš zájem o účast stále trvá.</p><p>Přesný program ještě ladíme. Můžete sledovat náš <a href=\"\\&quot;http://www.nakopnetesvojiskolu.cz/\\&quot;\">web</a> nebo <a href=\"\\&quot;https://www.facebook.com/nakopnete.svoji.skolu/\\&quot;\">Facebook</a>.</p>" },
                new Text() { Id = 3, Name = "Email po zaplacení", Subject = "Nakopněte svoji školu - potvrzení přijetí platby", Content = "<p>Potvrzujeme přijetí vaší platby za registrační poplatek na konferenci Nakopněte svoji školu 24. - 26. 2. 2019.</p>" }
            );
        }
    }
}
