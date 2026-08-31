using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Features.ForUsersFeatures.ArtistsDetails.Result;

namespace SingHub.Application.Features.ForUsersFeatures.ArtistsDetails.Queries;

public record ArtistDetailsAndSongQuery(int Id) : IRequest<BaseResult<ArtistDetailsAndSongQueryResult>>;
