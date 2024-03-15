using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GestionUsuarios.APPLICATION.Common.Exceptions;

[Serializable]
public class GeneralException : Exception
{
    private const string DefaultMessage = "La entidad no existe.";
    public GeneralException() : base(DefaultMessage)
    {
    }

    public GeneralException(string message) : base(message)
    {
    }


    public GeneralException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected GeneralException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
