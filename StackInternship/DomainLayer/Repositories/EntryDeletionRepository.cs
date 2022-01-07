using DataLayer.Entities;
using DataLayer.Entities.Models;
using DomainLayer.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class EntryDeletionRepository
    {
        private readonly StackInternshipDbContext context;
        public EntryDeletionRepository()
        {
            context = DbContextFactory.GetStackInternshipDbContext();
        }

        public void DeleteEntry(Entry chosenEntry)
        {
            DecrementParentsCommentCount(chosenEntry.ParentId);
            DeleteChildren(chosenEntry.Id);
            DeleteUserEntries(chosenEntry.Id);
            context.Remove(context.Entries.Find(chosenEntry.Id));
            context.SaveChanges();
        }

        public void DecrementParentsCommentCount(int parentId)
        {
            if (parentId is not 0)
            {
                var parent = context.Entries
                       .Find(parentId);
                parent.CommentCount--;
            }
        }

        public void DeleteChildren(int parentId)
        {
            var comments = context.Entries
                .Where(e => e.ParentId == parentId)
                .ToList();
            foreach (var comment in comments)
            {
                DeleteUserEntries(comment.Id);
                if (comment.CommentCount is not 0)
                {
                    DeleteChildren(comment.Id);
                }
                context.Remove(comment);
            }
        }

        public void DeleteUserEntries(int entryId)
        {
            var userEntries = context.UserEntries
                .Where(ue => ue.EntryId == entryId)
                .ToList();

            foreach (var userEntry in userEntries)
            {
                context.Remove(userEntry);
            }
        }
    }
}
