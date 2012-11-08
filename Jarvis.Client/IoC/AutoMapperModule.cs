using AutoMapper;
using AutoMapper.Mappers;
using Ninject;
using Ninject.Modules;
using Jarvis.Client.Model;
using DTO = Jarvis.WCF.Contracts.Data;

namespace Jarvis.Client.IoC
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITypeMapFactory>().To<TypeMapFactory>();
            foreach (var mapper in MapperRegistry.AllMappers())
                Bind<IObjectMapper>().ToConstant(mapper);
            Bind<ConfigurationStore>().ToSelf().InSingletonScope()
                .WithConstructorArgument("mappers",
                    ctx => ctx.Kernel.GetAll<IObjectMapper>());
            Bind<IConfiguration>().ToMethod(ctx => ctx.Kernel.Get<ConfigurationStore>());
            Bind<IConfigurationProvider>().ToMethod(ctx =>
                ctx.Kernel.Get<ConfigurationStore>());
            Bind<IMappingEngine>().To<MappingEngine>();

            MapDomainToDto(Kernel.Get<IConfiguration>());
        }

        private void MapDomainToDto(IConfiguration configuration)
        {
            configuration.CreateMap<DTO.Location, Location>();
            configuration.CreateMap<DTO.LocationCategory, LocationCategory>();
            configuration.CreateMap<DTO.Action, Action>()
                .Include<DTO.ExecuteProgramAction, ExecuteProgramAction>()
                .Include<DTO.ShowMessageAction, ShowMessageAction>()
                .Include<DTO.TerminateProgramAction, TerminateProgramAction>();
            configuration.CreateMap<DTO.ExecuteProgramAction, ExecuteProgramAction>();
            configuration.CreateMap<DTO.ShowMessageAction, ShowMessageAction>();
            configuration.CreateMap<DTO.TerminateProgramAction, TerminateProgramAction>();


            configuration.CreateMap<Location, DTO.Location>();
            configuration.CreateMap<LocationCategory, DTO.LocationCategory>();
            configuration.CreateMap<Action, DTO.Action>()
                .Include<ExecuteProgramAction, DTO.ExecuteProgramAction>()
                .Include<ShowMessageAction, DTO.ShowMessageAction>()
                .Include<TerminateProgramAction, DTO.TerminateProgramAction>();
            configuration.CreateMap<ExecuteProgramAction, DTO.ExecuteProgramAction>();
            configuration.CreateMap<ShowMessageAction, DTO.ShowMessageAction>();
            configuration.CreateMap<TerminateProgramAction, DTO.TerminateProgramAction>();

        }
    }
}
