using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CqsLibrary.Interfaces
{
    public interface IQueryHandler<T, Q> where Q : IRequest<T>
    {
    }
}
