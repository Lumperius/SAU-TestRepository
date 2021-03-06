﻿using ContextLibrary.DataContexts;
using CqsLibrary.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace CqsLibrary.Queries.NewsQueries
{
    public class GetNewsById : IRequest<News>
    {
        public  Guid ID { get; } 
        public GetNewsById(Guid id)
        {
            ID = id;
        }
    }
}
