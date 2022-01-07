using DataLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Factories;
using DataLayer.Entities;
using DataLayer.Entities.Models;
using DomainLayer;
using DomainLayer.Models;

namespace PresentationLayer
{
    public class EntryService
    {
        public static bool ResourceActionsMenuHeader(User loggedInUser, EntryDepartmentChoice departmentChoice, ListResourcesType listResourcesType)
        {
            EntryRepository er = new();
            var entryDetails = er.GetEntryDetailsList(departmentChoice, loggedInUser, EntryType.Resource, 0);
            if (entryDetails is null)
            {
                StringHelper.OutputPainter($"Željena kategorija je prazna!", ConsoleColor.Red, ConsoleColor.Black);
                return false;
            }
            foreach (var entryDetail in entryDetails)
            {
                if (listResourcesType is ListResourcesType.Unanswered
                    && entryDetail.CommentCount is not 0) continue;
                EntryPrinter.PrintPrimaryEntry(entryDetail);
                GetAnswers(loggedInUser, departmentChoice, entryDetail);
                Console.WriteLine("\n======================================================================================\n\n");
            }
            return true;
        }

        public static void AnswerActionsMenuHeader(User loggedInUser, Entry answer)
        {
            EntryRepository er = new();
            EntryPrinter.PrintPrimaryEntry(er.GetEntryDetails(answer.Id, answer.AuthorId));

            var listOfComments = er.GetEntryDetailsList(answer.Department, loggedInUser, EntryType.Comment, answer.Id);

            foreach (var comment in listOfComments)
            {
                EntryPrinter.PrintSecondaryEntry(comment, EntryType.Comment);
            }
        }

        public static void GetAnswers(User loggedInUser, EntryDepartmentChoice departmentChoice, EntryDetails entryDetail)
        {
            EntryRepository er = new();
            if (entryDetail.CommentCount != 0)
            {
                StringHelper.OutputPainter("\tKomentari:\n", ConsoleColor.DarkGray, ConsoleColor.Black);
            }
            foreach (var comment in er.GetEntryDetailsList(departmentChoice, loggedInUser, EntryType.Answer, entryDetail.Id))
            {
                EntryPrinter.PrintSecondaryEntry(comment, EntryType.Answer);
            }
        }

        public static void MakeNewEntry(User loggedInUser, EntryDepartmentChoice departmentChoice, int parentId, EntryType entryType)
        {
            Console.Clear();
            if (!EntryHelper.AddingNewEntryCheck(loggedInUser.ReputationPoints, entryType))
            {
                return;
            }
            var content = EnterContent();
            if (content is null)
            {
                return;
            }
            EntryRepository er = new();
            er.AddNewEntry(loggedInUser, departmentChoice, parentId, entryType, content);
            PopupPrinter.SuccessfulEntry();
        }

        public static int ChooseEntryTointeractWith()
        {
            Console.WriteLine("\nUnesite ID objave s kojom želite interakciju:\n" +
                            "(za povratak unesite 0)");
            if (!int.TryParse(Console.ReadLine().Trim(), out int entryToInteractId))
            {
                StringHelper.OutputPainter("Molimo unesite cijeli broj!", ConsoleColor.Red, ConsoleColor.Black);
                entryToInteractId = ChooseEntryTointeractWith();
            }
            return entryToInteractId;
        }

        public static Entry GetEntryAvailableForInteraction(EntryDepartmentChoice departmentChoice, EntryType entryType, ListResourcesType listResourcesType)
        {
            var entryToInteractId = ChooseEntryTointeractWith();
            if (entryToInteractId is 0)
            {
                Console.Clear();
                return null;
            }
            EntryRepository er = new();
            List<Entry> availableEntries = new();
            if (listResourcesType is ListResourcesType.Regular && entryType is EntryType.Resource)
            {
                availableEntries = er.GetAvailableEntries(departmentChoice);
            }
            if (listResourcesType is ListResourcesType.Regular && entryType is EntryType.Answer)
            {
                availableEntries = er.GetAvailableAnswers(departmentChoice);
            }
            if (listResourcesType is ListResourcesType.Unanswered)
            {
                availableEntries = er.GetAvailableUnansweredEntries(departmentChoice);
            }
            var chosenEntry = availableEntries
                .FirstOrDefault(e => e.Id == entryToInteractId);
            if (chosenEntry is null)
            {
                StringHelper.OutputPainter("Unijeli ste ID koji ne postoji u ovom ispisu! Molimo ponovite unos.", ConsoleColor.Red, ConsoleColor.Black);
                return GetEntryAvailableForInteraction(departmentChoice, entryType, listResourcesType);
            }
            return chosenEntry;
        }

        public static Entry GetAnswerAndCommentsAvailableForInteraction(Entry answer)
        {
            var entryToInteractId = ChooseEntryTointeractWith();
            if (entryToInteractId is 0)
            {
                Console.Clear();
                return null;
            }
            if (answer.Id == entryToInteractId)
            {
                return answer;
            }

            EntryRepository er = new();

            var chosenEntry = er.GetComments(answer.Id)
                .FirstOrDefault(e => e.Id == entryToInteractId);

            if (chosenEntry is null)
            {
                StringHelper.OutputPainter("Unijeli ste ID koji ne postoji u ovom ispisu! Molimo ponovite unos.", ConsoleColor.Red, ConsoleColor.Black);
                return GetAnswerAndCommentsAvailableForInteraction(answer);
            }
            return chosenEntry;
        }

        public static string EnterContent()
        {
            Console.WriteLine("Unesite sadržaj:" +
                "(za odustajanje unesite prazan string)");
            var content = Console.ReadLine().Trim();
            if (content.Length is 0)
            {
                PopupPrinter.GiveUpOnChoosing();
                return null;
            }
            if (content.Length < 10)
            {
                StringHelper.OutputPainter("Sadržaj ne smije biti kraći od 10 znakova! " +
                    "Molimo ponovite unos.", ConsoleColor.Red, ConsoleColor.Black);
                return EnterContent();
            }
            return content;
        }

        public static void AddComment(User loggedInUser, Entry chosenEntry)
        {
            Console.Clear();
            if (loggedInUser.ReputationPoints < 3)
            {
                PopupPrinter.NotEnoughPointsToComment();
                return;
            }
            Console.Clear();
            var content = EnterContent();
            if (content is null)
            {
                return;
            }
            EntryRepository er = new();
            er.AddNewEntry(loggedInUser, chosenEntry.Department, chosenEntry.Id, chosenEntry.TypeOfEntry+1, content);
            PopupPrinter.SuccessfulComment();
        }

        public static void EditEntry(User loggedInUser, Entry chosenEntry)
        {
            Console.Clear();
            if (!EntryHelper.EditingPrivilegeCheck(loggedInUser, chosenEntry))
            {
                PopupPrinter.ReturnToResources();
                return;
            }
            Console.Clear();
            var content = EnterContent();
            if (content is null)
            {
                return;
            }

            EntryRepository er = new();
            er.EditEntry(chosenEntry, loggedInUser, content);
            PopupPrinter.SuccessfulEdit();
        }

        public static void DeleteEntry(User loggedInUser, Entry chosenEntry)
        {
            if (!EntryHelper.DeletingEntryCheck(loggedInUser, chosenEntry))
            {
                return;
            }
            EntryDeletionRepository edr = new();
            edr.DeleteEntry(chosenEntry);
            PopupPrinter.SuccessfulDelete();
        }

        public static Entry GetAccessibleEntryForResourceActionsMenu(EntryActionChoice choice, EntryDepartmentChoice departmentChoice, ListResourcesType listResourcesType)
        {
            if (choice is EntryActionChoice.Upvote || choice is EntryActionChoice.Downvote || choice is EntryActionChoice.Edit
                || choice is EntryActionChoice.Delete || choice is EntryActionChoice.AnswerResource)
            {
                return GetEntryAvailableForInteraction(departmentChoice, EntryType.Resource, listResourcesType);
            }
            if (choice is EntryActionChoice.ViewComments && listResourcesType is ListResourcesType.Regular)
            {
                return GetEntryAvailableForInteraction(departmentChoice, EntryType.Answer, listResourcesType);
            }
            if (choice is EntryActionChoice.AddNewResource)
            {
                return new Entry();
            }
            return null;
        }

        public static Entry GetAccessibleEntryForAnswerActionsMenu(AnswerActionChoice choice, Entry answer)
        {

            if (choice is AnswerActionChoice.Upvote || choice is AnswerActionChoice.Downvote
                || choice is AnswerActionChoice.Edit || choice is AnswerActionChoice.Delete)
            {
                return GetAnswerAndCommentsAvailableForInteraction(answer);
            }
            if (choice is AnswerActionChoice.CommentAnswer)
            {
                return new Entry();
            }
            return null;
        }
    }
}
