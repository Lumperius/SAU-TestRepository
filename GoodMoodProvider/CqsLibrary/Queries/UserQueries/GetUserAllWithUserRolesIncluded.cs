using CqsLibrary.Interfaces;
using MediatR;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace CqsLibrary.Queries.UserQueries
{
    public class GetUserAllWithUserRolesIncluded : IRequest<IEnumerable<User>>
    {
    }
}
