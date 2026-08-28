namespace SingHub.Application.Features.ForAdminFeatures.Dashboard.GetRoleBasedUserDistribution.Result;
public class GetRoleBasedUserDistributionQueryResult
{
    public string RoleName { get; set; }
    public int UserCount { get; set; }
    public string AccessLevel { get; set; }
    public string Status { get; set; }
}