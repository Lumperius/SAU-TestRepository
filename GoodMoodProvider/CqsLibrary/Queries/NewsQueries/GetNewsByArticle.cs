using CqsLibrary.Interfaces;
using MediatR;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace CqsLibrary.Queries.NewsQueries
{
    public class GetNewsByArticle : IRequest<News>
    {
        public string Article { get; }

        public GetNewsByArticle(string article)
        {
            Article = article;
        }
    }
}
