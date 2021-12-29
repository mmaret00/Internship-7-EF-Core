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
                        UserName = "boze123",
                        Password = "sifra",
                        ReputationPoints = 1,
                        Role = UserRole.Intern,
                        IsTrustedUser = false,
                    },
                    new User
                    {
                        Id = 7,
                        UserName = "pzelic",
                        Password = "asdas",
                        ReputationPoints = 10000,
                        Role = UserRole.Organizer,
                        IsTrustedUser = true,
                    }
                });

            builder.Entity<Resource>()
                .HasData(new List<Resource>
                {
                    new Resource
                    {
                        Id = 1,
                        AuthorId = 1,
                        Department = ResourceDepartment.General,
                        DateOfPublishing = new DateTime(2021, 12, 1),
                        Content = "Prva obavijest",
                    },
                    new Resource
                    {
                        Id = 2,
                        AuthorId = 3,
                        Department = ResourceDepartment.Development,
                        DateOfPublishing = new DateTime(2021, 12, 1),
                        Content = "Kad je iduće predavanje?",
                    },
                    new Resource
                    {
                        Id = 3,
                        AuthorId = 2,
                        Department = ResourceDepartment.Design,
                        DateOfPublishing = new DateTime(2021, 12, 25),
                        Content = "Zašto mi se ne ispiše ništa kad stavim where?",
                    },
                    new Resource
                    {
                        Id = 4,
                        AuthorId = 3,
                        Department = ResourceDepartment.Development,
                        DateOfPublishing = new DateTime(2021, 12, 1),
                        Content = "Upute za LINQ!",
                    },
                });
        }
    }
}
