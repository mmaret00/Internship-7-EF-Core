﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Enums
{
    public enum VoteResult
    {
        Successful,
        OwnEntry,
        AlreadyVoted,
        NotEnoughPointsToUpvote,
        NotEnoughPointsToDownvoteResource,
        NotEnoughPointsToDownvoteComment,
    }
}
