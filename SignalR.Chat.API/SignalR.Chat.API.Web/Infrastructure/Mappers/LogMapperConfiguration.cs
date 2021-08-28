using SignalR.Chat.API.Entities;
using SignalR.Chat.API.Web.Infrastructure.Mappers.Base;
using SignalR.Chat.API.Web.ViewModels.LogViewModels;
using Calabonga.UnitOfWork;

namespace SignalR.Chat.API.Web.Infrastructure.Mappers
{
    /// <summary>
    /// Mapper Configuration for entity Log
    /// </summary>
    public class LogMapperConfiguration : MapperConfigurationBase
    {
        /// <inheritdoc />
        public LogMapperConfiguration()
        {
            CreateMap<LogCreateViewModel, Log>()
                .ForMember(x => x.Id, o => o.Ignore());

            CreateMap<Log, LogViewModel>();

            CreateMap<IPagedList<Log>, IPagedList<LogViewModel>>()
                .ConvertUsing<PagedListConverter<Log, LogViewModel>>();
        }
    }
}