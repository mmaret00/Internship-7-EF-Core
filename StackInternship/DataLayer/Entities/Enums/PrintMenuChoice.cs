using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Enums
{
    public enum PrintMenuChoice
    {
        AllUsers = '1',
        UsersWithSpecificRole = '2',
        TrustedUsers = '3',
        MoreThanReputationPoints = '4',
        LessThanReputationPoints = '5',
        Return = '0'
    }
}
