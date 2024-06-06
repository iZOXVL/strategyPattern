using Api.Microeservice.Autor.Aplicacion;
using Api.Microeservice.Autor.Persistencia;
using AutoMapper;
using MediatR;

namespace Api.Microservice.Autor.Aplicacion
{
    public class ConsultarFiltro
    {
        public class AutorUnico : IRequest<AutorDto>
        {
            public string AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDto>
        {
            private readonly ContextoAutor _context;
            private readonly IMapper _mapper;
            private readonly IAutorStrategy _autorStrategy;

            public Manejador(ContextoAutor context, IMapper mapper, IAutorStrategy autorStrategy)
            {
                _context = context;
                _mapper = mapper;
                _autorStrategy = autorStrategy;
            }

            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                return await _autorStrategy.GetAutorPorGuid(_context, _mapper, request.AutorGuid);
            }
        }
    }
}
