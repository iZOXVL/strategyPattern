using Api.Microeservice.Autor.Aplicacion;
using Api.Microeservice.Autor.Modelo;
using Api.Microeservice.Autor.Persistencia;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Microservice.Autor.Aplicacion
{
    public class EstrategiasConcretas : IAutorStrategy
    {
        public async Task<List<AutorDto>> GetAutores(ContextoAutor context, IMapper mapper)
        {
            var autores = await context.AutorLibros.ToListAsync();
            var autoresDto = mapper.Map<List<AutorLibro>, List<AutorDto>>(autores);
            return autoresDto;
        }

        public async Task<AutorDto> GetAutorPorGuid(ContextoAutor context, IMapper mapper, string autorGuid)
        {
            var autor = await context.AutorLibros
                .Where(p => p.AutorLibroGuid == autorGuid).FirstOrDefaultAsync();
            if (autor == null)
            {
                throw new Exception("No se encontró el autor");
            }
            var autorDto = mapper.Map<AutorLibro, AutorDto>(autor);
            return autorDto;
        }

        public async Task<Unit> CrearAutor(ContextoAutor context, Nuevo.Ejecuta request)
        {
            var autorLibro = new AutorLibro
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                FechaNacimiento = request.FechaNacimiento,
                AutorLibroGuid = Convert.ToString(Guid.NewGuid())
            };

            context.AutorLibros.Add(autorLibro);
            var respuesta = await context.SaveChangesAsync();
            if (respuesta > 0)
            {
                return Unit.Value;
            }

            throw new Exception("No se pudo insertar el Autor del libro");
        }

        public Task Validate(Nuevo.Ejecuta request)
        {
            var validator = new InlineValidator<Nuevo.Ejecuta>();
            validator.RuleFor(p => p.Nombre).NotEmpty();
            validator.RuleFor(p => p.Apellido).NotEmpty();

            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return Task.CompletedTask;
        }
    }
}
