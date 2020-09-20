using ContextLibrary.DataContexts;
using CqsLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CqsLibrary.Queries
{
    public class GetNewsByIdQuery 
    {
        public  Guid ID { get; } 
        public GetNewsByIdQuery(Guid id)
        {
            ID = id;
        }
    }
}
