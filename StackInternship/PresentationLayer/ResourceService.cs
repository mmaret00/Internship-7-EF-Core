using DataLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Factories;
using DataLayer.Entities;
using DataLayer.Entities.Models;

namespace PresentationLayer
{
    public class ResourceService
    {
        public StackInternshipDbContext context;
        public ResourceService()
        {
            context = DbContextFactory.GetStackInternshipDbContext();
        }

        static public void DepartmentMenu(User loggedInUser)
        {
            while (true)
            {
                var choice = (ResourceDepartment)DepartmentMenuOutput();

                switch (choice)
                {
                    case ResourceDepartment.Development:
                    case ResourceDepartment.Design:
                    case ResourceDepartment.Marketing:
                    case ResourceDepartment.Multimedia:
                    case ResourceDepartment.General:
                        ResourceActionsMenu(loggedInUser, choice);
                        PopupService.ReturnToDepartments();
                        break;
                    case ResourceDepartment.Exit:
                        PopupService.ReturnToDashboard();
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Molimo unesite jedan od dopuštenih brojeva (0 - 5)\n");
                        break;
                }
            }
        }

        static public char DepartmentMenuOutput()
        {
            Console.WriteLine($"Odaberite akciju:\n" +
                "1 - Dev\n" +
                "2 - Dizajn\n" +
                "3 - Marketing\n" +
                "4 - Multimedija\n" +
                "5 - Generalno\n" +
                "0 - Povratak na dashboard");

            char.TryParse(Console.ReadLine().Trim(), out char choice);

            return choice;
        }

        public void ListResourcesOfADepartment(ResourceDepartment choice)
        {
            Console.Clear();
            var govno = context.Users;
            context.Entries
                .OrderByDescending(c => c.DateOfPublishing)
                .Where(c => c.Department == choice)
                .Join(govno, e => e.AuthorId, u => u.Id, (entry, user) => new
                {
                    Entry = entry,
                    User = user
                })
                .ToList()
                .ForEach(c => {
                    Console.WriteLine($"Id: {c.Entry.Id}\n" +
                        $"Kategorija: {c.Entry.Department}\n" +
                        $"Autor: {c.User.UserName}\n" +
                        $"Datum objave: {c.Entry.DateOfPublishing}\n" +
                        $"Sadržaj: {c.Entry.Content}\n" +
                        $"Upvoteovi: {c.Entry.UpvoteCount}\n" +
                        $"Downvoteovi: {c.Entry.DownvoteCount}\n\n");
                });
        }

        static public void ResourceActionsMenu(User loggedInUser, ResourceDepartment departmentChoice)
        {
            while (true)
            {
                ResourceService us = new();
                us.ListResourcesOfADepartment(departmentChoice);
                var choice = (ResourceAction)ResourceActionsMenuOutput();

                switch (choice)
                {
                    case ResourceAction.Interaction:
                        Console.WriteLine("\nUnesite ID resursa s kojim želite interakciju:");
                        int.TryParse(Console.ReadLine().Trim(), out int resourceToInteractId);
                        ResourceActionsSubmenu(loggedInUser, resourceToInteractId);
                        break;
                    case ResourceAction.Exit:
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Molimo unesite jedan od dopuštenih brojeva (0 - 5)\n");
                        break;
                }
            }
        }

        static public char ResourceActionsMenuOutput()
        {
            Console.WriteLine($"Odaberite akciju:\n" +
                $"1 - Interakcija s pojedinim resursom\n" +
                "0 - Povratak na izbor kategorija");

            char.TryParse(Console.ReadLine().Trim(), out char choice);

            return choice;
        }

        static public void ResourceActionsSubmenu(User loggedInUser, int resourceToInteractId)
        {
            while (true)
            {
                var choice = (ResourceSubaction)ResourceActionsSubmenuOutput();

                switch (choice)
                {
                    case ResourceSubaction.Upvote:
                    case ResourceSubaction.Downvote:
                        ResourceService rs = new();
                        rs.VoteOnAResource(loggedInUser, resourceToInteractId, choice);
                        return;
                    case ResourceSubaction.Exit:
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Molimo unesite jedan od dopuštenih brojeva (0 - 3)\n");
                        break;
                }
            }
        }

        static public char ResourceActionsSubmenuOutput()
        {
            Console.WriteLine("\nOdaberite akciju:\n" +
                "1 - Upvote\n" +
                "2 - Downvote\n" +
                "3 - Pregled komentara\n" +
                "0 - Povratak na pregled resursa\n");

            char.TryParse(Console.ReadLine().Trim(), out char choice);

            return choice;
        }

        public void VoteOnAResource(User loggedInUser, int resourceToInteractId, ResourceSubaction choice)
        {
            int authorId = 0;
            Entry chosenEntry = null;
            foreach (var entry in context.Entries)
            {
                if(entry.Id == resourceToInteractId)
                {
                    if (choice == ResourceSubaction.Upvote)
                        entry.UpvoteCount++;
                    else if (choice == ResourceSubaction.Downvote)
                        entry.DownvoteCount++;
                    
                    authorId = entry.AuthorId;
                    chosenEntry = entry;
                    break;
                }
            }
            var alreadyVoted = context
                 .UserEntries
                 .Where(c => c.User.Id == loggedInUser.Id && c.Entry.Id == chosenEntry.Id)
                 .All(c => c.Upvoted == true);

            if(alreadyVoted)
            {
                Console.WriteLine("Već ste glasali za ovo!");
                PopupService.ClickAnyKeyToContinue();
                return;
            }

            ChangeReputationPointsOnAccountOfAVote(authorId, loggedInUser.Id, choice);
            Console.WriteLine("Uspješno ste glasali.");
            PopupService.ClickAnyKeyToContinue();
            context.SaveChanges();
        }

        public void ChangeReputationPointsOnAccountOfAVote(int authorId, int voterId, ResourceSubaction choice)
        {
            foreach (var user in context.Users)
            {
                if (user.Id == voterId)
                {
                    if (choice == ResourceSubaction.Upvote)
                        user.ReputationPoints += 10;
                    else if (choice == ResourceSubaction.Downvote)
                        user.ReputationPoints--;
                }
                if (user.Id == authorId)
                {
                    if (choice == ResourceSubaction.Upvote)
                        user.ReputationPoints += 15;
                    else if (choice == ResourceSubaction.Downvote)
                        user.ReputationPoints -= 2;
                }
            }
            context.SaveChanges();
        }
    }
}
