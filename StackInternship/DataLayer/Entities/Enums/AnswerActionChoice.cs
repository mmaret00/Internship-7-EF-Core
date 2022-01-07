using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Enums
{
    public enum AnswerActionChoice
    {
        Upvote = '1',
        Downvote = '2',
        CommentAnswer = '3',
        Edit = '4',
        Delete = '5',
        Return = '0',
    }
}
