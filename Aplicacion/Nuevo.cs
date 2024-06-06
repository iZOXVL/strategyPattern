using Api.Microeservice.Autor.Modelo;
using Api.Microeservice.Autor.Persistencia;
using FluentValidation;
using MediatR;

namespace Api.Microservice.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoAutor _context;
            private readonly IAutorStrategy _autorStrategy;

            public Manejador(ContextoAutor context, IAutorStrategy autorStrategy)
            {
                _context = context;
                _autorStrategy = autorStrategy;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellation)
            {
                await _autorStrategy.Validate(request);
                return await _autorStrategy.CrearAutor(_context, request);
            }
        }
    }
}
