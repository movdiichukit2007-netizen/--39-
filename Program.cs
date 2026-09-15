using PostalApi.Dtos;
using PostalApi.Mappings;
using PostalApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPostingService, PostingService>();

var app = builder.Build();

app.MapGet("/postings", (IPostingService service) =>
{
    var postings = service.GetAll();
    var result = postings.Select(PostingMapper.ToPostingGetDto).ToList();
    return Results.Ok(result);
});

app.MapGet("/postings/{id:int}", (int id, IPostingService service) =>
{
    var posting = service.Find(id);

    return posting is not null
        ? Results.Ok(PostingMapper.ToPostingGetDto(posting))
        : Results.NotFound();
});

app.MapPost("/postings", (PostingPostDto dto, IPostingService service) =>
{
    var posting = PostingMapper.ToPosting(dto);
    var saved = service.Create(posting);
    var result = PostingMapper.ToPostingGetDto(saved);

    return Results.Created($"/postings/{result.Id}", result);
});

app.MapPut("/postings/{id:int}", (int id, PostingPutDto dto, IPostingService service) =>
{
    dto.Id = id;

    var posting = PostingMapper.ToPosting(dto);
    var updated = service.Update(posting);

    return updated is not null
        ? Results.Ok(PostingMapper.ToPostingGetDto(updated))
        : Results.NotFound();
});

app.MapDelete("/postings/{id:int}", (int id, IPostingService service) =>
{
    var deleted = service.Delete(id);

    return deleted ? Results.NoContent() : Results.NotFound();
});

app.Run();
