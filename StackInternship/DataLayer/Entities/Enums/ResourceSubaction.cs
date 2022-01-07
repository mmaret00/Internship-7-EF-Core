﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Enums
{
    public enum ResourceSubaction
    {
        Upvote = '1',
        Downvote = '2',
        AddComment = '3',
        ViewComments = '4',
        Edit = '5',
        Delete = '6',
        Exit = '0'
    }
}
