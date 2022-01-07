using DataLayer.Entities.Enums;
using DataLayer.Entities.Models;
using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class EntryHelper
    {
        public static void VotingResultCheck(VoteResult result)
        {
            switch (result)
            {
                case VoteResult.Successful:
                    PopupPrinter.SuccessfulVote();
                    break;
                case VoteResult.OwnEntry:
                    PopupPrinter.VoteForOwnEntry();
                    break;
                case VoteResult.AlreadyVoted:
                    PopupPrinter.AlreadyVoted();
                    break;
                case VoteResult.NotEnoughPointsToUpvote:
                    PopupPrinter.NotEnoughPoints(5, EntryActionChoice.Upvote);
                    break;
                case VoteResult.NotEnoughPointsToDownvoteResource:
                    PopupPrinter.NotEnoughPoints(20, EntryActionChoice.Downvote);
                    break;
                case VoteResult.NotEnoughPointsToDownvoteComment:
                    PopupPrinter.NotEnoughPoints(15, EntryActionChoice.Downvote);
                    break;
                default:
                    break;
            }
        }

        public static void FindPopularEntries(User loggedInUser)
        {
            Console.Clear();
            EntryRepository er = new();
            if (!er.CheckIfAnyResourcesWereMadeToday())
            {
                PopupPrinter.NoEntriesMadeToday();
                return;
            }
            var popularEntries = er.GetPopularEntries(loggedInUser);
            Console.WriteLine("Najpopularniji današnji resursi:\n");
            foreach (var entry in popularEntries)
            {
                EntryPrinter.PrintPrimaryEntry(entry);
            }
            PopupPrinter.ReturnToDashboard();
        }

        public static bool AddingNewEntryCheck(int reputationPoints, EntryType typeOfEntry)
        {
            var success = typeOfEntry is EntryType.Resource || reputationPoints >= 3;
            if (!success)
            {
                PopupPrinter.NotEnoughPointsToComment();
            }
            return success;
        }

        public static bool EditingPrivilegeCheck(User loggedInUser, Entry chosenEntry)
        {
            if (loggedInUser.Id != chosenEntry.AuthorId && loggedInUser.Role != UserRole.Organizer)
            {
                StringHelper.OutputPainter("Budući da ste intern, možete uređivati samo svoje objave!"
                    , ConsoleColor.Red, ConsoleColor.Black);
                return false;
            }
            if (chosenEntry.AuthorId != loggedInUser.Id && loggedInUser.ReputationPoints < 250)
            {
                StringHelper.OutputPainter("Budući da imate manje od 250 bodova, ne možete uređivati " +
                    "tuđe objave!", ConsoleColor.Red, ConsoleColor.Black);
                return false;
            }
            if (chosenEntry.AuthorId == loggedInUser.Id && loggedInUser.ReputationPoints < 150)
            {
                StringHelper.OutputPainter("Budući da imate manje od 150 bodova, ne možete uređivati " +
                    "vlastite objave!", ConsoleColor.Red, ConsoleColor.Black);
                return false;
            }
            return true;
        }

        public static bool DeletingPrivilegeCheck(User loggedInUser, Entry chosenEntry)
        {
            if (loggedInUser.ReputationPoints < 500)
            {
                StringHelper.OutputPainter("Budući da imate manje od 500 reputacijskih bodova, nemate privilegiju brisanja objave!", ConsoleColor.Red, ConsoleColor.Black);
                return false;
            }
            if (loggedInUser.Id != chosenEntry.AuthorId && loggedInUser.Role != UserRole.Organizer)
            {
                StringHelper.OutputPainter("Budući da ste intern, možete brisati samo svoje objave!", ConsoleColor.Red, ConsoleColor.Black);
                return false;
            }
            return true;
        }

        public static bool DeletingEntryCheck(User loggedInUser, Entry chosenEntry)
        {
            if (chosenEntry is null)
            {
                return false;
            }
            if (!DeletingPrivilegeCheck(loggedInUser, chosenEntry))
            {
                PopupPrinter.ReturnToResources();
                return false;
            }
            Console.WriteLine("Jeste li sigurni da želite obrisati ovu objavu?");
            if (!StringHelper.ConfirmationCheck())
            {
                PopupPrinter.ReturnToResources();
                return false;
            }
            return true;
        }

        public static EntryActionChoice ConvertVotingChoice(AnswerActionChoice choice)
        {
            if (choice is AnswerActionChoice.Upvote)
            {
                return EntryActionChoice.Upvote;
            }
            return EntryActionChoice.Downvote;
        }

        public static bool CheckIfAnswerHasComments(Entry chosenEntry)
        {
            var success = chosenEntry.CommentCount is not 0;
            if (!success)
            {
                StringHelper.OutputPainter("Odgovor nema komentara!", ConsoleColor.Red, ConsoleColor.Black);
                PopupPrinter.ReturnToResources();
            }
            return success;
        }
    }
}
