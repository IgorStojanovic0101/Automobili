using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Commands.Images;
using Template.Application.Queries.Images;

namespace Template.Application.Queries
{
    public class Query(IMediator mediator)
    {
        private readonly IMediator _mediator = mediator;
        //   var task = await new DeleteImagesOlderThanCommandHandler().Handle(command);

        public async Task<int> DeleteImagesQueryAsync(string symbol, DateTime cutoffDate) => 
            await _mediator.Send(new DeleteImagesOlderThanQuery(symbol, cutoffDate));

    }
}
