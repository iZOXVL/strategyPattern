using Api.Microeservice.Autor.Aplicacion;
using Api.Microeservice.Autor.Persistencia;
using Api.Microservice.Autor.Aplicacion;
using AutoMapper;
using MediatR;

public interface IAutorStrategy
{
    Task<List<AutorDto>> GetAutores(ContextoAutor context, IMapper mapper);
    Task<AutorDto> GetAutorPorGuid(ContextoAutor context, IMapper mapper, string autorGuid);
    Task<Unit> CrearAutor(ContextoAutor context, Nuevo.Ejecuta request);
    Task Validate(Nuevo.Ejecuta request);
}
