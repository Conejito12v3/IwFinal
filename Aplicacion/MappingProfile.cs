using Aplicacion.ClasesDto;
using Aplicacion.Productos;
using AutoMapper;
using Dominio;

namespace Aplicacion;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Dominio.Producto, ProductoDto>()
            .ForMember(x => x.productor, y => y.MapFrom(z => z.productor))
            .ForMember(x => x.categoria, y => y.MapFrom(z => z.categoria));
        CreateMap<Dominio.Productor, ProductorDto>()
            .ForMember(x => x.redesSociales, y => y.MapFrom(z => z.redesSociales));
        CreateMap<Dominio.RedesSociales, RedesSocialesDto>();
        CreateMap<Dominio.CategoriaProducto, CategoriaProductoDto>();
    }
}