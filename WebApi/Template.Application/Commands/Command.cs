using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Commands.Images;

namespace Template.Application.Commands
{
    public class Command(IMediator mediator)
    {
        private readonly IMediator _mediator = mediator;
        //   var task = await new DeleteImagesOlderThanCommandHandler().Handle(command);

        public async Task DeleteImagesAsync(string symbol, DateTime cutoffDate) => 
            await _mediator.Send(new DeleteImagesOlderThanCommand(symbol, cutoffDate));

    }
}
