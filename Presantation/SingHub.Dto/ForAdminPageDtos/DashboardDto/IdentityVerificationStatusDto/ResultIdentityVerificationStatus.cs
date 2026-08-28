namespace SingHub.Dto.ForAdminPageDtos.DashboardDto.IdentityVerificationStatusDto;
public class ResultIdentityVerificationStatus
{
    public double ConfirmedEmailPercentage { get; set; }
    public int ConfirmedEmailCount { get; set; }
    public int TwoFactorEnabledCount { get; set; }
    public int LockedOutUserCount { get; set; }
}
