using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using DataLayer.Entities;
using DataLayer.Entities.Enums;
using DataLayer.Entities.Models;

namespace DataLayer
{
    public class DbSeeder
    {
        public static void Execute(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasData(new List<User>
                {
                    new User
                    {
                        Id = 1,
                        UserName = "MateaB",
                        Password = "12345",
                        ReputationPoints = 10,
                        Role = UserRole.Intern,
                        IsTrustedUser = true,
                    },
                    new User
                    {
                        Id = 2,
                        UserName = "lovre",
                        Password = "54321",
                        ReputationPoints = 44444,
                        Role = UserRole.Organizer,
                        IsTrustedUser = true,
                        UserEntries = null,
                    },
                    new User
                    {
                        Id = 3,
                        UserName = "bartol_deak",
                        Password = "qweqw",
                        ReputationPoints = 1,
                        Role = UserRole.Intern,
                        IsTrustedUser = false,
                    },
                    new User
                    {
                        Id = 4,
                        UserName = "mmaretic",
                        Password = "qwqwq",
                        ReputationPoints = 1000,
                        Role = UserRole.Intern,
                        IsTrustedUser = true,
                    },
                    new User
                    {
                        Id = 5,
                        UserName = "anamarija",
                        Password = "password",
                        ReputationPoints = 1,
                        Role = UserRole.Intern,
                        IsTrustedUser = false,
                    },
                    new User
                    {
                        Id = 6,
                        UserName = "boze topic",
                        Password = "sifra",
                        ReputationPoints = 1,
                        Role = UserRole.Intern,
                        IsTrustedUser = false,
                    },
                    new User
                    {
                        Id = 7,
                        UserName = "petra123",
                        Password = "asdas",
                        ReputationPoints = 10000,
                        Role = UserRole.Organizer,
                        IsTrustedUser = true,
                    }
                });

            builder.Entity<Entry>()
                .HasData(new List<Entry>
                {
                    new Entry
                    {
                        Id = 1,
                        AuthorId = 1,
                        Department = ResourceDepartment.General,
                        DateOfPublishing = new DateTime(2021, 12, 1, 8, 0, 0),
                        Content = "Prva obavijest",
                    },
                    new Entry
                    {
                        Id = 2,
                        AuthorId = 3,
                        Department = ResourceDepartment.Development,
                        DateOfPublishing = new DateTime(2021, 12, 1, 12, 12, 12),
                        Content = "Kad je iduće predavanje?",
                    },
                    new Entry
                    {
                        Id = 3,
                        AuthorId = 2,
                        Department = ResourceDepartment.Design,
                        DateOfPublishing = new DateTime(2021, 12, 25, 14, 30, 0),
                        Content = "Zašto mi se ne ispiše ništa kad stavim where?",
                    },
                    new Entry
                    {
                        Id = 4,
                        AuthorId = 3,
                        Department = ResourceDepartment.Development,
                        DateOfPublishing = new DateTime(2021, 12, 1, 0, 30, 0),
                        Content = "Upute za LINQ!",
                    },
                    new Entry
                    {
                        Id = 5,
                        AuthorId = 3,
                        Department = ResourceDepartment.General,
                        DateOfPublishing = new DateTime(2021, 12, 1, 23, 59, 59),
                        Content = "Uskršnji party je u učionici na Veliki petak u 19h, nemojte kasnit!",
                    },
                });

            builder.Entity<Comment>()
               .HasData(new List<Comment>
               {
                    new Comment
                    {
                        Id = 100,
                        AuthorId = 2,
                        ParentId = 1,
                        DateOfPublishing = new DateTime(2021, 12, 1),
                        Content = "ok",
                    },
                });

            /*builder.Entity<UserEntry>()
              .HasData(new List<UserEntry>
              {
                    new UserEntry
                    {
                        Id = 1,
                        UserId = 2,
                        EntryId = 2,
                    },
               });*/
        }
    }
}
