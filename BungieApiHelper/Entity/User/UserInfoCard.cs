using System.Collections.Generic;

namespace BungieApiHelper.Entity.User
{
    public class UserInfoCard
    {
        public string supplementalDisplayName { get; set; }
        public string iconPath { get; set; }
        public int crossSaveOverride { get; set; }
        public List<int> applicableMembershipTypes { get; set; }
        public bool isPublic { get; set; }
        public int membershipType { get; set; }
        public int membershipid { get; set; }
        public string displayName { get; set; }
    }
}