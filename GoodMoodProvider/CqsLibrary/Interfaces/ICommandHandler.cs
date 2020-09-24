using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CqsLibrary.Interfaces
{
    public interface ICommandHandler<T, C> where C : IRequest
    {
    }
}
