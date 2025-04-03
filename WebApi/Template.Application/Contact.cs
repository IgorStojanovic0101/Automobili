using Template.Application.Services;
using System.Net;
using Template.Application.Commands;
using Template.Application.Queries;

namespace Template.Application
{
    public interface ITemplateClient
    { 
        Service Service { get; }
        Command Command { get; }
        Query Query { get; } 

        //Request
        //Response

    }

    public sealed class TemplateClient(Service service, Command command, Query query) : ITemplateClient
    {
      
      
        public Service Service { get; } = service;
        public Command Command { get; } = command;
        public Query Query { get; } = query;

        //Request
        //Response


    }
}
