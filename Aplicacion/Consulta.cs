using Api.Microeservice.Autor.Aplicacion;
using Api.Microeservice.Autor.Persistencia;
using AutoMapper;
using MediatR;

namespace Api.Microservice.Autor.Aplicacion
{
    public class Consulta
    {
        public class ListaAutor : IRequest<List<AutorDto>> { }

        public class Manejador : IRequestHandler<ListaAutor, List<AutorDto>>
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

            public async Task<List<AutorDto>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                return await _autorStrategy.GetAutores(_context, _mapper);
            }
        }
    }
}
