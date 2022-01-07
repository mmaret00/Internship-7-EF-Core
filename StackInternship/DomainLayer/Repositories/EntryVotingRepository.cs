using DataLayer.Entities;
using DataLayer.Entities.Enums;
using DataLayer.Entities.Models;
using DomainLayer.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class EntryVotingRepository
    {
        private readonly StackInternshipDbContext context;
        public EntryVotingRepository()
        {
            context = DbContextFactory.GetStackInternshipDbContext();
        }

        public VoteResult VoteOnAnEntry(User loggedInUser, Entry chosenEntry, EntryActionChoice choice)
        {
            var chosenUserEntry = context.UserEntries
                .Where(ue => ue.UserId == loggedInUser.Id)
                .Where(ue => ue.EntryId == chosenEntry.Id)
                .FirstOrDefault();

            chosenEntry = context.Entries.Find(chosenEntry.Id);
            var success = VotingPrivilegeCheck(loggedInUser, chosenEntry, chosenUserEntry, choice);
            if (success is not VoteResult.Successful)
            {
                return success;
            }
            ChangeVoteCountOfAnEntry(chosenEntry, choice, chosenUserEntry);
            ChangeReputationPointsOnAccountOfAVote(chosenEntry.AuthorId, loggedInUser.Id, choice);

            context.SaveChanges();
            return success;
        }

        public static VoteResult VotingPrivilegeCheck(User loggedInUser, Entry chosenEntry, UserEntry chosenUserEntry, EntryActionChoice choice)
        {
            if (chosenEntry.AuthorId == loggedInUser.Id)
            {
                return VoteResult.OwnEntry;
            }
            if (chosenUserEntry.Voted is true)
            {
                return VoteResult.AlreadyVoted;
            }
            if (loggedInUser.ReputationPoints < 5 && choice is EntryActionChoice.Upvote)
            {
                return VoteResult.NotEnoughPointsToUpvote;
            }
            if (loggedInUser.ReputationPoints < 15 && choice is EntryActionChoice.Downvote && chosenEntry.TypeOfEntry is not EntryType.Resource)
            {
                return VoteResult.NotEnoughPointsToDownvoteComment;
            }
            if (loggedInUser.ReputationPoints < 20 && choice is EntryActionChoice.Downvote && chosenEntry.TypeOfEntry is EntryType.Resource)
            {
                return VoteResult.NotEnoughPointsToDownvoteResource;
            }
            return VoteResult.Successful;
        }

        public void ChangeVoteCountOfAnEntry(Entry chosenEntry, EntryActionChoice choice, UserEntry chosenUserEntry)
        {
            chosenEntry = context.Entries.Find(chosenEntry.Id);
            if (choice is EntryActionChoice.Upvote)
            {
                chosenEntry.UpvoteCount++;
            }
            else if (choice is EntryActionChoice.Downvote)
            {
                chosenEntry.DownvoteCount++;
            }
            chosenUserEntry.Voted = true;
        }

        public void ChangeReputationPointsOnAccountOfAVote(int? authorId, int voterId, EntryActionChoice choice)
        {
            var author = context.Users.Find(authorId);
            var voter = context.Users.Find(voterId);

            if (choice is EntryActionChoice.Upvote)
            {
                voter.ReputationPoints += 10;
                ChangeInternsStatusOnAccountOfReputationPoints(voter);
                author.ReputationPoints += 15;
                ChangeInternsStatusOnAccountOfReputationPoints(author);
            }
            else if (choice is EntryActionChoice.Downvote)
            {
                voter.ReputationPoints--;
                KeepReputationPointsPositive(voter);
                author.ReputationPoints -= 2;
                KeepReputationPointsPositive(author);
            }
        }

        public static void ChangeInternsStatusOnAccountOfReputationPoints(User user)
        {
            if (user.IsTrustedUser is false && user.ReputationPoints >= 1000)
            {
                user.IsTrustedUser = true;
            }
            if (user.Role is UserRole.Intern && user.ReputationPoints >= 100000)
            {
                user.Role = UserRole.Organizer;
                user.IsTrustedUser = true;
            }
        }

        public static void KeepReputationPointsPositive(User user)
        {
            if (user.ReputationPoints < 1)
            {
                user.ReputationPoints = 1;
            }
        }
    }
}
