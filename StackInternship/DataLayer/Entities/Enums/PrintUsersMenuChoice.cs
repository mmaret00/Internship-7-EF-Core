using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Enums
{
    public enum PrintUsersMenuChoice
    {
        AllUsers = '1',
        OnlyOrganizers = '2',
        OnlyInterns = '3',
        TrustedUsers = '4',
        MoreThanReputationPoints = '5',
        LessThanReputationPoints = '6',
        Return = '0'
    }
}
