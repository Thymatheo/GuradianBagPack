using BungieApiHelper.Entity.Ignore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BungieApiHelper.Entity.User
{
    public class UserToUserContext
    {
        public bool isFollowing { get; set; }
        public IgnoreResponse ignoreStatus { get; set; }
        public DateTime globalIgnoreEndDate { get; set; }
    }
}
