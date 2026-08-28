namespace SingHub.Dto.ForAdminPageDtos.DashboardDto.GetRoleBasedUserDistributionDto;
public class ResultGetRoleBasedUserDistribution
{
    public string RoleName { get; set; }
    public int UserCount { get; set; }
    public string AccessLevel { get; set; }
    public string Status { get; set; }
}
