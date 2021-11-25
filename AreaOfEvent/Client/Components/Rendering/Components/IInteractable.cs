using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AreaOfEvent.Client.Components.Rendering.Components
{
    public interface IInteractable
    {
        //TODO why task
        Task CheckHit( ClickConext ctx );
    }

    public interface IInteractable<T> : IInteractable
    {
        public EventCallback<T> OnHit { get; set; }
    }


}
