using Microsoft.AspNetCore.Components;
using System;
using System.Runtime.Serialization;

namespace AreaOfEvent.Client.Services.Modal
{
    public abstract class ModalMapperServiceException : Exception
    {
        protected ModalMapperServiceException()
        { }

        protected ModalMapperServiceException( string message ) : base( message )
        { }

        protected ModalMapperServiceException( SerializationInfo info, StreamingContext context ) : base( info, context )
        { }

        protected ModalMapperServiceException( string message, Exception innerException ) : base( message, innerException )
        { }
    }

    public class InvalidMapToTypeException : ModalMapperServiceException
    {
        public InvalidMapToTypeException( Type t ) : base( $"{t.FullName} must implement the {typeof( IComponent ).FullName} interface to be mappable" )
        { }
    }

    public class UnmappedTypeException : ModalMapperServiceException
    {
        public UnmappedTypeException( Type t ) : base( $"{t.FullName} is not mapped" )
        { }
    }

}
