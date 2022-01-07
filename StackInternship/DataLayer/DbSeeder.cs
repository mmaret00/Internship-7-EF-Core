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
                    },
                    new User
                    {
                        Id = 2,
                        UserName = "lovre",
                        Password = "54321",
                        ReputationPoints = 444444,
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
                    },
                    new User
                    {
                        Id = 4,
                        UserName = "mmaretic",
                        Password = "qwqwq",
                        ReputationPoints = 990,
                        Role = UserRole.Intern,
                    },
                    new User
                    {
                        Id = 5,
                        UserName = "Anamarija",
                        Password = "password",
                        ReputationPoints = 1,
                        Role = UserRole.Intern,
                    },
                    new User
                    {
                        Id = 6,
                        UserName = "Boze Topic",
                        Password = "sifra",
                        ReputationPoints = 1,
                        Role = UserRole.Intern,
                    },
                    new User
                    {
                        Id = 7,
                        UserName = "petra123",
                        Password = "asdas",
                        ReputationPoints = 200000,
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
                        Department = EntryDepartmentChoice.General,
                        PublishedAt = new DateTime(2021, 12, 1, 8, 0, 0),
                        Content = "Integer ac neque. Duis bibendum. Morbi non quam nec dui luctus rutrum. Nulla tellus. In sagittis dui vel nisl.",
                        CommentCount = 1,
                        TypeOfEntry = EntryType.Resource,
                    },
                    new Entry
                    {
                        Id = 2,
                        AuthorId = 3,
                        Department = EntryDepartmentChoice.Development,
                        PublishedAt = new DateTime(2021, 12, 1, 12, 12, 12),
                        Content = "Integer pede justo, lacinia eget, tincidunt eget, tempus vel, pede. Morbi porttitor lorem id ligula. Suspendisse ornare consequat lectus. In est risus, auctor sed, tristique in, tempus sit amet, sem. Fusce consequat. Nulla nisl. Nunc nisl.",
                        TypeOfEntry = EntryType.Resource,
                    },
                    new Entry
                    {
                        Id = 3,
                        AuthorId = 2,
                        Department = EntryDepartmentChoice.Design,
                        PublishedAt = new DateTime(2021, 12, 25, 14, 30, 0),
                        Content = "Vivamus vestibulum sagittis sapien. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Etiam vel augue. Vestibulum rutrum rutrum neque.",
                        TypeOfEntry = EntryType.Resource,
                    },
                    new Entry
                    {
                        Id = 4,
                        AuthorId = 3,
                        Department = EntryDepartmentChoice.Development,
                        PublishedAt = new DateTime(2021, 12, 1, 0, 30, 0),
                        Content = "Duis consequat dui nec nisi volutpat eleifend. Donec ut dolor. Morbi vel lectus in quam fringilla rhoncus. Mauris enim leo, rhoncus sed, vestibulum sit amet, cursus id, turpis. Integer aliquet, massa id lobortis convallis, tortor risus dapibus augue, vel accumsan tellus nisi eu orci. Mauris lacinia sapien quis libero. Nullam sit amet turpis elementum ligula vehicula consequat.",
                        TypeOfEntry = EntryType.Resource,
                    },
                    new Entry
                    {
                        Id = 5,
                        AuthorId = 3,
                        Department = EntryDepartmentChoice.General,
                        PublishedAt = new DateTime(2021, 12, 1, 23, 59, 59),
                        Content = "Aenean auctor gravida sem. Praesent id massa id nisl venenatis lacinia. Aenean sit amet justo. Morbi ut odio. Cras mi pede, malesuada in, imperdiet et, commodo vulputate, justo. In blandit ultrices enim.",
                        TypeOfEntry = EntryType.Resource,
                    },
                    new Entry
                    {
                        Id = 6,
                        AuthorId = 3,
                        ParentId = 1,
                        Department = EntryDepartmentChoice.General,
                        PublishedAt = new DateTime(2022, 1, 1, 12, 13, 14),
                        Content = "Quisque erat eros, viverra eget.",
                        CommentCount = 1,
                        TypeOfEntry = EntryType.Answer,
                    },
                    new Entry
                    {
                        Id = 7,
                        AuthorId = 4,
                        ParentId = 6,
                        Department = EntryDepartmentChoice.General,
                        PublishedAt = new DateTime(2022, 1, 3, 12, 13, 14),
                        Content = "Maecenas ut massa quis augue luctus tincidunt.",
                        TypeOfEntry = EntryType.Comment,
                    },
                });
        }
    }
}
