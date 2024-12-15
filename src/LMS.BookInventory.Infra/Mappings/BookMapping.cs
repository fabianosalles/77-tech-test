using AutoMapper;

namespace LMS.BookInventory.Infra.Mappings;

/// <summary>
/// Automapper mappings for Book and Domain entities
/// </summary>
public class BookMapping: Profile
{
    public BookMapping()
    {
        CreateBookProfile();
    }

    /// <summary>
    /// Mapping between db entity book and the domain book
    /// </summary>
    private void CreateBookProfile()
    {
        CreateMap<Database.Models.Book, Domain.Entities.Book>()
            .ForMember(f => f.Id, f => f.MapFrom(m => m.Id))
            .ForMember(f => f.Isbn, f => f.MapFrom(m => m.Isbn))
            .ForMember(f => f.Name, f => f.MapFrom(m => m.Name))
            .ForMember(f => f.Author, f => f.MapFrom(m => m.Author))
            .ForMember(f => f.Edition, f => f.MapFrom(m => m.Edition))
            .ForMember(f => f.Description, f => f.MapFrom(m => m.Description))
            .ForMember(f => f.Publisher, f => f.MapFrom(m => m.Publisher));

        CreateMap<Domain.Entities.Book, Database.Models.Book>()
            .ForMember(f => f.Id, f => f.MapFrom(m => m.Id))
            .ForMember(f => f.Isbn, f => f.MapFrom(m => m.Isbn))
            .ForMember(f => f.Name, f => f.MapFrom(m => m.Name))
            .ForMember(f => f.Author, f => f.MapFrom(m => m.Author))
            .ForMember(f => f.Edition, f => f.MapFrom(m => m.Edition))
            .ForMember(f => f.Description, f => f.MapFrom(m => m.Description))
            .ForMember(f => f.Publisher, f => f.MapFrom(m => m.Publisher))            
            .ForMember(f => f.LastUpdatedDateTime, f => f.Ignore())
            .ForMember(f => f.Deleted, f => f.Ignore())
            .ForMember(f => f.RowVersion, f => f.Ignore());

    }
}