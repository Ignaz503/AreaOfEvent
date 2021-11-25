using System;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;

namespace AreaOfEvent.Client.Services.Modal
{
    public interface IModalMapperService
    {
        IModalReference ShowForType( IModalService modalService, Type type, string title = null, ModalParameters parameters = null, ModalOptions options = null );

        IModalReference ShowForType<T>( IModalService modalService, string title = null, ModalParameters parameters = null, ModalOptions options = null );
    }
}
