using DataLayer.Entities;
using DataLayer.Entities.Enums;
using DataLayer.Entities.Models;
using DomainLayer.Factories;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class EntryRepository 
    {
        private readonly StackInternshipDbContext context;
        public EntryRepository()
        {
            context = DbContextFactory.GetStackInternshipDbContext();
        }

        public void AddNewUserEntry(Entry entry, User loggedInUser)
        {
            var possibleUserEntry = context.UserEntries
            .Where(uen => uen.EntryId == entry.Id && uen.UserId == loggedInUser.Id)
            .FirstOrDefault();

            if (possibleUserEntry is null)
            {
                var newUserEntry = new UserEntry(loggedInUser.Id, entry.Id);
                entry.ViewCount++;
                context.Add(newUserEntry);
                context.SaveChanges();
            }
        }

        public void AddNewEntry(User loggedInUser, EntryDepartmentChoice departmentChoice, int parentId, EntryType entryType, string content)
        {
            context.Entries.Add(new Entry(loggedInUser.Id, departmentChoice, content, parentId, entryType));

            if (parentId is not 0)
            {
                var parent = context.Entries
                       .FirstOrDefault(e => e.Id == parentId);
                parent.CommentCount++;
            }
            context.SaveChanges();
        }

        public bool CheckIfDepartmentIsEmpty(EntryDepartmentChoice departmentChoice)
        {
            return context.Entries
                .Where(e => e.Department == departmentChoice)
                .Any();
        }

        public bool CheckIfAnyResourcesWereMadeToday()
        {
            return context.Entries
                .Where(e => e.PublishedAt.Date == DateTime.Now.Date)
                .Any();
        }

        public List<EntryDetails> GetEntryDetailsList(EntryDepartmentChoice departmentChoice, User loggedInUser, EntryType entryType, int parentId)
        {
            if (!CheckIfDepartmentIsEmpty(departmentChoice))
            {
                return null;
            }

            List<EntryDetails> entryDetails = new();

            context.Entries
                .OrderByDescending(e => e.PublishedAt)
                .Where(e => e.Department == departmentChoice)
                .Where(e => e.TypeOfEntry == entryType)
                .Where(e => e.ParentId == parentId)
                .Join(context.Users, e => e.AuthorId, u => u.Id, (entry, user) => new
                {
                    Entry = entry,
                    User = user
                })
                .ToList()
                .ForEach(eu => {
                    AddNewUserEntry(eu.Entry, loggedInUser);
                    entryDetails.Add(GetEntryDetails(eu.Entry.Id, eu.User.Id));
                });

            return entryDetails;
        }

        public EntryDetails GetEntryDetails(int entryId, int? userId)
        {
            var userDetails = context.Entries
                .Include(e => e.Author)
                .Where(e => e.AuthorId == userId)
                .Select(e => new
                {
                    AuthorUserName = $"{e.Author.UserName}",
                    AuthorsRole = e.Author.Role,
                })
                .FirstOrDefault();

            var entrySpecificDetails = context.Entries
                .Where(e => e.Id == entryId)
                .Select(e => new
                {
                    e.Id,
                    e.Department,
                    Published = e.PublishedAt,
                    e.Content,
                    e.ViewCount,
                    e.UpvoteCount,
                    e.DownvoteCount,
                    e.CommentCount,
                    e.TypeOfEntry,
                })
                .FirstOrDefault();

            var entryDetails = new EntryDetails
            {
                Id = entrySpecificDetails.Id,
                AuthorUserName = userDetails.AuthorUserName,
                AuthorsRole = userDetails.AuthorsRole,
                Department = entrySpecificDetails.Department,
                Published = entrySpecificDetails.Published,
                Content = entrySpecificDetails.Content,
                ViewCount = entrySpecificDetails.ViewCount,
                UpvoteCount = entrySpecificDetails.UpvoteCount,
                DownvoteCount = entrySpecificDetails.DownvoteCount,
                CommentCount = entrySpecificDetails.CommentCount,
                TypeOfEntry = entrySpecificDetails.TypeOfEntry,
            };

            return entryDetails;
        }

        public void EditEntry(Entry chosenEntry, User loggedInUser, string content)
        {
            chosenEntry = context.Entries
                .Find(chosenEntry.Id);

            chosenEntry.Content = content + $" (uredio {DateTime.Now} korisnik {loggedInUser.UserName})";

            context.SaveChanges();
        }

        public List<Entry> GetAvailableUnansweredEntries(EntryDepartmentChoice departmentChoice)
        {
            return context.Entries
                .Where(e => e.TypeOfEntry == EntryType.Resource)
                .Where(e => e.CommentCount == 0)
                .Where(e => e.Department == departmentChoice)
                .ToList();
        }

        public List<Entry> GetAvailableEntries(EntryDepartmentChoice departmentChoice)
        {
            var availableResources = context.Entries
                .Where(e => e.TypeOfEntry == EntryType.Resource)
                .Where(e => e.Department == departmentChoice)
                .ToList();

            var idsOfAvailableResources = availableResources.Select(e => e.Id);

            var availableComments = context.Entries
                .Where(e => idsOfAvailableResources.Contains(e.ParentId))
                .ToList();

            availableResources.AddRange(availableComments);
            return availableResources;
        }

        public List<Entry> GetAvailableAnswers(EntryDepartmentChoice departmentChoice)
        {
            return context.Entries
                .Where(e => e.TypeOfEntry == EntryType.Answer)
                .Where(e => e.Department == departmentChoice)
                .ToList();
        }

        public List<Entry> GetComments(int parentId)
        {
            return context.Entries
                .Where(e => e.ParentId == parentId)
                .ToList();
        }

        public List<Entry> GetPopularEntries(EntryType entryType)
        {
            var availableResources = context.Entries
                .Where(e => e.TypeOfEntry == EntryType.Resource)
                .Where(e => e.PublishedAt.Date == DateTime.Now.Date)
                .ToList()
                .OrderByDescending(e => e.CommentCount)
                .ToList();

            var idsOfAvailableResources = availableResources.Select(e => e.Id);

            var availableComments = context.Entries
                .Where(e => idsOfAvailableResources.Contains(e.ParentId))
                .ToList();

            if (entryType is EntryType.Answer)
            {
                return availableComments;
            }

            availableResources.AddRange(availableComments);
            return availableResources.Take(5).ToList();
        }
    }
}
