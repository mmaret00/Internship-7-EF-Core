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

        static public User DepartmentMenu(User loggedInUser)
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
                        Console.Clear();
                        loggedInUser = ResourceActionsMenu(loggedInUser, choice);
                        PopupService.ReturnToDepartments();
                        break;
                    case ResourceDepartment.Exit:
                        PopupService.ReturnToDashboard();
                        return loggedInUser;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Molimo unesite jedan od dopuštenih brojeva (0 - 5)\n");
                        Console.ResetColor();
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

        public User ListResourcesOfADepartment(ResourceDepartment choice, User loggedInUser)
        {
            context.Entries
                .OrderByDescending(e => e.DateOfPublishing)
                .Where(e => e.Department == choice)
                .Join(context.Users, e => e.AuthorId, u => u.Id, (entry, user) => new
                {
                    Entry = entry,
                    User = user
                })
                .ToList()
                .ForEach(ue => {
                    //AddNewUserEntry(ue.Entry, loggedInUser);

                    //vanka fje
                    var possibleUserEntry = context.UserEntries
            .Where(uen => uen.EntryId == ue.Entry.Id && uen.UserId == loggedInUser.Id)
            .FirstOrDefault();
                    if (possibleUserEntry is null)
                    {
                        var newUserEntry = new UserEntry(loggedInUser.Id, ue.Entry.Id);
                        ue.Entry.ViewCount++;
                        context.Add(newUserEntry);
                        context.SaveChanges();
                    }
                    //vanka fje

                    Console.WriteLine($"Id: {ue.Entry.Id}\n" +
                        $"Kategorija: {ue.Entry.Department}\n" +
                        $"Autor: {ue.User.UserName}\n" +
                        $"Datum objave: {ue.Entry.DateOfPublishing}\n" +
                        $"Sadržaj: {ue.Entry.Content}\n" +
                        $"Broj pregleda: {ue.Entry.ViewCount}\n" +
                        $"Broj upvoteova: {ue.Entry.UpvoteCount}\n" +
                        $"Broj downvoteova: {ue.Entry.DownvoteCount}\n\n");
                    });
            return loggedInUser;
        }

        public Entry AddNewUserEntry(Entry entry, User loggedInUser)
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
            return entry;
        }

        static public User ResourceActionsMenu(User loggedInUser, ResourceDepartment departmentChoice)
        {
            while (true)
            {
                ResourceService us = new();
                loggedInUser = us.ListResourcesOfADepartment(departmentChoice, loggedInUser);
                var choice = (ResourceAction)ResourceActionsMenuOutput();
                ResourceService rs = new();

                switch (choice)
                {
                    case ResourceAction.Interaction:
                        Console.WriteLine("\nUnesite ID resursa s kojim želite interakciju:\n" +
                            "(za povratak unesite prazan unos)");
                        int.TryParse(Console.ReadLine().Trim(), out int resourceToInteractId);
                        if (resourceToInteractId is 0)
                        {
                            PopupService.ReturnToResources();
                            break;
                        }
                        var chosenEntry = rs.GetEntryFromId(resourceToInteractId, departmentChoice);
                        if (chosenEntry is null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unijeli ste ID koji ne postoji u ovoj kategoriji!");
                            Console.ResetColor();
                            PopupService.ReturnToResources();
                            break;
                        }
                        ResourceActionsSubmenu(loggedInUser, chosenEntry);
                        break;
                    case ResourceAction.AddNew:
                        rs.AddNewResource(loggedInUser, departmentChoice);
                        break;
                    case ResourceAction.Exit:
                        return loggedInUser;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Molimo unesite jedan od dopuštenih brojeva (0 - 2)\n");
                        Console.ResetColor();
                        break;
                }
            }
        }

        static public char ResourceActionsMenuOutput()
        {
            Console.WriteLine($"Odaberite akciju:\n" +
                $"1 - Interakcija s pojedinim resursom\n" +
                $"2 - Dodavanje novog resursa\n" +
                "0 - Povratak na izbor kategorija");

            char.TryParse(Console.ReadLine().Trim(), out char choice);

            return choice;
        }

        public Entry GetEntryFromId(int id, ResourceDepartment departmentChoice)
        {
            return context.Entries
                .Where(e => e.Department == departmentChoice && e.Id == id)
                .FirstOrDefault();
        }

        static public void ResourceActionsSubmenu(User loggedInUser, Entry chosenEntry)
        {
            while (true)
            {
                var choice = (ResourceSubaction)ResourceActionsSubmenuOutput();
                ResourceService rs = new();

                switch (choice)
                {
                    case ResourceSubaction.Upvote:
                    case ResourceSubaction.Downvote:
                        var changedUser = rs.VoteOnAResource(loggedInUser, chosenEntry, choice);
                        if (changedUser is not null) loggedInUser = changedUser;
                        return;
                    case ResourceSubaction.AddComment:
                        rs.AddNewComment(loggedInUser, chosenEntry);
                        break;
                    case ResourceSubaction.ViewComments:
                        rs.ShowComments(loggedInUser, chosenEntry);
                        break;
                    case ResourceSubaction.Edit:
                        rs.EditResource(loggedInUser, chosenEntry);
                        return;
                    case ResourceSubaction.Delete:
                        rs.DeleteResource(loggedInUser, chosenEntry);
                        return;
                    case ResourceSubaction.Exit:
                        return;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Molimo unesite jedan od dopuštenih brojeva (0 - 3)\n");
                        Console.ResetColor();
                        break;
                }
            }
        }

        static public char ResourceActionsSubmenuOutput()
        {
            Console.WriteLine("\nOdaberite akciju:\n" +
                "1 - Upvote\n" +
                "2 - Downvote\n" +
                "3 - Komentiraj\n" +
                "4 - Pregled komentara\n" +
                "5 - Uređivanje\n" +
                "6 - Brisanje\n" +
                "0 - Povratak na pregled resursa\n");

            char.TryParse(Console.ReadLine().Trim(), out char choice);

            return choice;
        }

        public User VoteOnAResource(User loggedInUser, Entry chosenEntry, ResourceSubaction choice)
        {
            var chosenUserEntry = context.UserEntries
            .Where(uen => uen.EntryId == chosenEntry.Id && uen.UserId == loggedInUser.Id)
            .FirstOrDefault();

            chosenEntry = context.Entries
                .Where(e => e == chosenEntry)
                .FirstOrDefault();

            if (chosenUserEntry.Voted is true)
            {
                Console.WriteLine("Već ste glasali za ovo!");
                PopupService.ReturnToResources();
                return null;
            }
            chosenUserEntry.Voted = true;
            context.SaveChanges();

            int authorId = chosenEntry.AuthorId;

            if (choice == ResourceSubaction.Upvote)
                chosenEntry.UpvoteCount++;
            else if (choice == ResourceSubaction.Downvote)
                chosenEntry.DownvoteCount++;


            ChangeReputationPointsOnAccountOfAVote(authorId, loggedInUser.Id, choice);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Uspješno ste glasali.");
            Console.ResetColor();
            PopupService.ClickAnyKeyToContinue();
            context.SaveChanges();
            return loggedInUser;
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

        public void AddNewResource(User loggedInUser, ResourceDepartment departmentChoice)
        {
            Console.Clear();
            Console.WriteLine("Unesite sadržaj novog resursa:" +
                "(za odustajanje unesite prazan string)");
            var content = Console.ReadLine().Trim();
            if (content is null) {
                PopupService.GiveUp();
                return; 
            }
            context.Entries.Add(new Entry(loggedInUser.Id, departmentChoice, content));
            context.SaveChanges();
            PopupService.SuccessfulEntry();
        }

        public void EditResource(User loggedInUser, Entry chosenEntry)
        {
            Console.Clear();

            if (!EditingPrivilegeCheck(loggedInUser, chosenEntry))
            {
                PopupService.ReturnToResources();
                return;
            }

            Console.WriteLine("Unesite novi sadržaj:");
            var content = Console.ReadLine().Trim();
            if (content.Length < 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sadržaj ne smije biti kraći od 10 znakova!");
                Console.ResetColor();
                PopupService.ReturnToResources();
                return;
            }//u odvojenu fju

            chosenEntry = GetEntryFromId(chosenEntry.Id, chosenEntry.Department);
            chosenEntry.Content = content + $" (uređeno {DateTime.Now} od strane korisnika {loggedInUser.UserName})";

            context.SaveChanges();
            PopupService.SuccessfulEdit();
        }

        public bool EditingPrivilegeCheck(User loggedInUser, Entry chosenEntry)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (loggedInUser.Id != chosenEntry.AuthorId && loggedInUser.Role != UserRole.Organizer)
            {
                Console.WriteLine("Budući da ste intern, možete uređivati samo svoje objave!");
                return false;
            }
            if (chosenEntry.AuthorId != loggedInUser.Id && loggedInUser.ReputationPoints < 250)
            {
                Console.WriteLine("Budući da imate manje od 250 bodova," +
                    " ne možete uređivati tuđe objave!");
                return false;
            }
            if (chosenEntry.AuthorId == loggedInUser.Id && loggedInUser.ReputationPoints < 150)
            {
                Console.WriteLine("Budući da imate manje od 150 bodova," +
                    " ne možete uređivati vlastite objave!");
                return false;
            }
            Console.ResetColor();
            return true;
        }

        public void DeleteResource(User loggedInUser, Entry chosenEntry)
        {
            Console.Clear();

            if(!DeletingPrivilegeCheck(loggedInUser, chosenEntry))
            {
                PopupService.ReturnToResources();
                return;
            }

            chosenEntry = context.Entries
                .Where(e => e == chosenEntry)
                .FirstOrDefault();
            context.Remove(chosenEntry);

            context.SaveChanges();
            PopupService.SuccessfulDelete();
        }

        public bool DeletingPrivilegeCheck(User loggedInUser, Entry chosenEntry)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (loggedInUser.ReputationPoints < 500)
            {
                Console.WriteLine("Budući da imate manje od 500 reputacijskih bodova, " +
                    "nemate privilegiju brisanja komentara!");
                return false;
            }
            if (loggedInUser.Id != chosenEntry.AuthorId && loggedInUser.Role != UserRole.Organizer)
            {
                Console.WriteLine("Budući da ste intern, možete brisati samo svoje objave!");
                return false;
            }
            Console.ResetColor();
            return true;
        }

        public void AddNewComment(User loggedInUser, Entry parent)
        {
            Console.Clear();
            Console.WriteLine("Unesite sadržaj komentara:" +
                "(za odustajanje unesite prazan string)");
            var content = Console.ReadLine().Trim();
            if (content is null)
            {
                PopupService.GiveUp();
                return;
            }
            context.Comments.Add(new Comment(loggedInUser.Id, parent.Id, content, parent.Department));
            context.SaveChanges();
            PopupService.SuccessfulEntry();
        }

        public void ShowComments(User loggedInUser, Entry chosenEntry)
        {
            context.Comments
                .OrderByDescending(c => c.DateOfPublishing)
                .Join(context.Users, c => c.AuthorId, u => u.Id, (comment, user) => new
                {
                    Comment = comment,
                    User = user
                })
                .Join(context.Entries, cu => cu.Comment.ParentId, e => e.Id, (usercomment, entry) => new
                {
                    UserComment = usercomment,
                    Entry = entry
                })
                .Where(cue => cue.Entry == chosenEntry)
                .ToList()
                .ForEach(cue => {
                    //AddNewUserEntry(ue.Entry, loggedInUser);

                    //vanka fje
                    var possibleUserEntry = context.UserEntries
            .Where(uen => uen.EntryId == cue.Entry.Id && uen.UserId == loggedInUser.Id)
            .FirstOrDefault();
                    if (possibleUserEntry is null)
                    {
                        var newUserEntry = new UserEntry(loggedInUser.Id, cue.Entry.Id);
                        cue.Entry.ViewCount++;
                        context.Add(newUserEntry);
                        context.SaveChanges();
                    }
                    //vanka fje

                    Console.WriteLine($"Id: {cue.UserComment.Comment.Id}\n" +
                        $"Autor: {cue.UserComment.Comment.AuthorId}\n" +
                        $"Datum objave: {cue.UserComment.Comment.DateOfPublishing}\n" +
                        $"Sadržaj: {cue.UserComment.Comment.Content}\n" +
                        $"Broj pregleda: {cue.UserComment.Comment.ViewCount}\n" +
                        $"Broj upvoteova: {cue.UserComment.Comment.UpvoteCount}\n" +
                        $"Broj downvoteova: {cue.UserComment.Comment.DownvoteCount}\n\n");
                });
            PopupService.ClickAnyKeyToReturn();
        }
    }
}
