namespace SingHub.Dto.ForAdminPageDtos.ArtistsDto;

public class ResultArtistsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Biography { get; set; }
    public string ImageUrl { get; set; }
    public string BannerUrl { get; set; }
    public string Country { get; set; }
    public DateTime BirthDate { get; set; }
    public int ArtistBySongCount { get; set; }
    public int ArtistByAlbumCount { get; set; }
}
