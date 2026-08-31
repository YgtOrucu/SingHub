namespace SingHub.Dto.ForPresantationPageDtos.SongsDto;
public class ResultSongsDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Duration { get; set; }
    public string AudioUrl { get; set; }
    public string? CoverImageUrl { get; set; }
}
