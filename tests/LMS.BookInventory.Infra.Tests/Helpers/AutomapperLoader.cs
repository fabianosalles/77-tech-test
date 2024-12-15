using AutoMapper;

namespace LMS.BookInventory.Infra.UnitTests.Helpers;

/// <summary>
/// Loads automapper from assembly
/// </summary>
public static class AutomapperLoader
{
    /// <summary>
    /// Loads automapper configuration from infrastructure assemblies 
    /// </summary>
    /// <returns></returns>
    public static IMapper LoadInfrastructure()
    {
        const string infraAssembly = "LMS.BookInventory.Infra";

        var mapper = new MapperConfiguration(cfg => { cfg.AddMaps(infraAssembly); }).
                CreateMapper();

        return mapper;
    }
}