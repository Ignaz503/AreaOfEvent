using System;
using System.Collections.Generic;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;

namespace AreaOfEvent.Client.Services.Modal
{
    public class ModalMapperService : IModalMapperService
    {
        Dictionary<Type,Type> mapping = new();

        public void Register( Type from, Type to )
        {
            if (!typeof( IComponent ).IsAssignableFrom( to ))
                throw new InvalidMapToTypeException( to );

            if (!mapping.ContainsKey( from ))
                mapping.Add( from, to );
        }

        public void Register<TFrom, TTo>() where TTo : IComponent
        {
            if (!mapping.ContainsKey( typeof( TFrom ) ))
            {
                mapping.Add( typeof( TFrom ), typeof( TTo ) );
            }
        }

        public IModalReference ShowForType( IModalService modalService, Type type, string title = null, ModalParameters parameters = null, ModalOptions options = null )
        {
            if (mapping.ContainsKey( type ))
                return modalService.Show( mapping[type], title, parameters, options );
            throw new UnmappedTypeException( type );
        }

        public IModalReference ShowForType<T>( IModalService modalService, string title = null, ModalParameters parameters = null, ModalOptions options = null )
            => ShowForType( modalService, typeof( T ), title, parameters, options );
    }

}
