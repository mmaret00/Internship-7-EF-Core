using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Enums
{
    public enum EntryActionChoice
    {
        Upvote = '1',
        Downvote = '2',
        AddNewResource = '3',
        AnswerResource = '4',
        Edit = '5',
        Delete = '6',
        ViewComments = '7',
        Return = '0',
    }
}
