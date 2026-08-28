namespace SingHub.Application.Features.ForAdminFeatures.Dashboard.GetIdentityVerificationStatus.Result;
public class GetIdentityVerificationStatusQueryResult
{
        public double ConfirmedEmailPercentage { get; set; }
        public int ConfirmedEmailCount { get; set; }
        public int TwoFactorEnabledCount { get; set; }
        public int LockedOutUserCount { get; set; }
}
