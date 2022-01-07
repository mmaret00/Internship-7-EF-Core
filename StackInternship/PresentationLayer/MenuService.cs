using DataLayer.Entities.Enums;
using DataLayer.Entities.Models;
using DomainLayer;
using PresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class MenuService
    {
        public static void LoginMenu()
        {
            while (true)
            {
                var choice = (LoginMenuChoice)MenuOutputPrinter.LoginMenuOutput();

                switch (choice)
                {
                    case LoginMenuChoice.Login:
                    case LoginMenuChoice.Register:
                        var loggedInUser = LoginService.EnterUsersInfo(choice);
                        if (loggedInUser is not null)
                        {
                            DashboardMenu(loggedInUser);
                        }
                        break;
                    case LoginMenuChoice.Exit:
                        return;
                    default:
                        PopupPrinter.UnallowedEntry(2);
                        break;
                }
            }
        }

        public static void PrintUsersMenu()
        {
            while (true)
            {
                var choice = (PrintUsersMenuChoice)MenuOutputPrinter.PrintUsersMenuOutput();

                switch (choice)
                {
                    case PrintUsersMenuChoice.AllUsers:
                    case PrintUsersMenuChoice.OnlyOrganizers:
                    case PrintUsersMenuChoice.OnlyInterns:
                    case PrintUsersMenuChoice.TrustedUsers:
                    case PrintUsersMenuChoice.MoreThanReputationPoints:
                    case PrintUsersMenuChoice.LessThanReputationPoints:
                        UserPrinter.PrintUsers(choice);
                        break;
                    case PrintUsersMenuChoice.Return:
                        PopupPrinter.ReturnToDashboard();
                        return;
                    default:
                        PopupPrinter.UnallowedEntry(5);
                        break;
                }
            }
        }
        public static User ProfileMenu(User loggedInUser)
        {
            Console.Clear();
            while (true)
            {
                UserPrinter.ProfileMenuHeader(loggedInUser.Id);
                var choice = (ChangeUsersDataChoice)MenuOutputPrinter.ProfileMenuOutput();
                
                switch (choice)
                {
                    case ChangeUsersDataChoice.ChangeName:
                        Console.Clear();
                        loggedInUser = UserHelper.SetNewUserName(loggedInUser);
                        break;
                    case ChangeUsersDataChoice.ChangePassword:
                        loggedInUser = UserHelper.SetNewPassword(loggedInUser);
                        break;
                    case ChangeUsersDataChoice.Exit:
                        PopupPrinter.ReturnToDashboard();
                        return loggedInUser;
                    default:
                        PopupPrinter.UnallowedEntry(2);
                        break;
                }
            }
        }

        public static void DashboardMenu(User loggedInUser)
        {
            while (true)
            {
                var choice = (DashboardMenuChoice)MenuOutputPrinter.DashboardMenuOutput(loggedInUser.Role);

                switch (choice)
                {
                    case DashboardMenuChoice.Resources:
                        PopupPrinter.ContinueToDepartments();
                        DepartmentMenu(loggedInUser, ListResourcesType.Regular);
                        break;
                    case DashboardMenuChoice.Unanswered:
                        PopupPrinter.ContinueToDepartments();
                        DepartmentMenu(loggedInUser, ListResourcesType.Unanswered);
                        break;
                    case DashboardMenuChoice.Popular:
                        EntryHelper.FindPopularEntries(loggedInUser);
                        break;
                    case DashboardMenuChoice.Users:
                        Console.Clear();
                        PrintUsersMenu();
                        break;
                    case DashboardMenuChoice.MyProfile:
                        loggedInUser = ProfileMenu(loggedInUser);
                        break;
                    case DashboardMenuChoice.Deactivation:
                        Console.Clear();
                        if (loggedInUser.Role is UserRole.Organizer)
                        {
                            loggedInUser = OrganizerSubmenu(loggedInUser);
                        }
                        break;
                    case DashboardMenuChoice.Exit:
                        PopupPrinter.ReturnToLoginMenu();
                        return;
                    default:
                        PopupPrinter.UnallowedEntry(6);
                        break;
                }
            }
        }
        public static User OrganizerSubmenu(User loggedInUser)
        {
            while (true)
            {
                var choice = (OrganizerSubmenuChoice)MenuOutputPrinter.OrganizerSubmenuOutput();
                switch (choice)
                {
                    case OrganizerSubmenuChoice.DeactivateTemporarily:
                    case OrganizerSubmenuChoice.DeactivatePermanently:
                    case OrganizerSubmenuChoice.Reactivate:
                        DeactivationService.ChooseIntern(loggedInUser, choice);
                        break;
                    case OrganizerSubmenuChoice.Return:
                        PopupPrinter.ReturnToDashboard();
                        return loggedInUser;
                    default:
                        PopupPrinter.UnallowedEntry(2);
                        break;
                }
            }
        }
        public static void DepartmentMenu(User loggedInUser, ListResourcesType listResourcesType)
        {
            while (true)
            {
                var departmentChoice = (EntryDepartmentChoice)MenuOutputPrinter.DepartmentMenuOutput();

                switch (departmentChoice)
                {
                    case EntryDepartmentChoice.Development:
                    case EntryDepartmentChoice.Design:
                    case EntryDepartmentChoice.Marketing:
                    case EntryDepartmentChoice.Multimedia:
                    case EntryDepartmentChoice.General:
                        ResourceActionsMenu(loggedInUser, departmentChoice, listResourcesType);
                        PopupPrinter.ReturnToDepartments();
                        break;
                    case EntryDepartmentChoice.Exit:
                        PopupPrinter.ReturnToDashboard();
                        return;
                    default:
                        PopupPrinter.UnallowedEntry(5);
                        break;
                }
            }
        }

        public static void ResourceActionsMenu(User loggedInUser, EntryDepartmentChoice departmentChoice, ListResourcesType listResourcesType)
        {
            Console.Clear();
            while (true)
            {
                if (!EntryService.ResourceActionsMenuHeader(loggedInUser, departmentChoice, listResourcesType))
                {
                    return;
                }

                var choice = (EntryActionChoice)MenuOutputPrinter.EntryActionsMenuOutput(listResourcesType);
                var chosenEntry = EntryService.GetAccessibleEntryForResourceActionsMenu(choice, departmentChoice, listResourcesType);
                if (chosenEntry is null)
                {
                    return;
                }

                switch (choice)
                {
                    case EntryActionChoice.Upvote:
                    case EntryActionChoice.Downvote:
                        EntryVotingRepository evr = new();
                        var result = evr.VoteOnAnEntry(loggedInUser, chosenEntry, choice);
                        EntryHelper.VotingResultCheck(result);
                        break;
                    case EntryActionChoice.AddNewResource:
                        EntryService.MakeNewEntry(loggedInUser, departmentChoice, 0, EntryType.Resource);
                        break;
                    case EntryActionChoice.AnswerResource:
                        EntryService.AddComment(loggedInUser, chosenEntry);
                        break;
                    case EntryActionChoice.ViewComments:
                        if (listResourcesType is ListResourcesType.Unanswered)
                        {
                            PopupPrinter.UnallowedEntry(6);
                            break;
                        }
                        if (EntryHelper.CheckIfAnswerHasComments(chosenEntry))
                        {
                            AnswerActionsMenu(loggedInUser, chosenEntry);
                        }
                        break;
                    case EntryActionChoice.Edit:
                        EntryService.EditEntry(loggedInUser, chosenEntry);
                        break;
                    case EntryActionChoice.Delete:
                        EntryService.DeleteEntry(loggedInUser, chosenEntry);
                        break;
                    case EntryActionChoice.Return:
                        return;
                    default:
                        if (listResourcesType is ListResourcesType.Regular)
                        {
                            PopupPrinter.UnallowedEntry(7);
                        }
                        else
                        {
                            PopupPrinter.UnallowedEntry(6);
                        }
                        break;
                }
            }
        }

        public static void AnswerActionsMenu(User loggedInUser, Entry answer)
        {
            Console.Clear();
            while (true)
            {
                EntryService.AnswerActionsMenuHeader(loggedInUser, answer);
                var choice = (AnswerActionChoice)MenuOutputPrinter.AnswerActionsMenuOutput();
                var chosenEntry = EntryService.GetAccessibleEntryForAnswerActionsMenu(choice, answer);
                if (chosenEntry is null)
                {
                    return;
                }

                switch (choice)
                {
                    case AnswerActionChoice.Upvote:
                    case AnswerActionChoice.Downvote:
                        EntryVotingRepository evr = new();
                        var result = evr.VoteOnAnEntry(loggedInUser, chosenEntry, EntryHelper.ConvertVotingChoice(choice));
                        EntryHelper.VotingResultCheck(result);
                        break;
                    case AnswerActionChoice.CommentAnswer:
                        EntryService.MakeNewEntry(loggedInUser, answer.Department, answer.Id, EntryType.Comment);
                        break;
                    case AnswerActionChoice.Edit:
                        EntryService.EditEntry(loggedInUser, chosenEntry);
                        break;
                    case AnswerActionChoice.Delete:
                        EntryService.DeleteEntry(loggedInUser, chosenEntry);
                        break;
                    case AnswerActionChoice.Return:
                        Console.Clear();
                        return;
                    default:
                        PopupPrinter.UnallowedEntry(5);
                        break;
                }
            }
        }
    }
}
