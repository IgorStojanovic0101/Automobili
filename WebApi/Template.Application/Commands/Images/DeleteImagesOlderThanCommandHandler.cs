using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Application.Commands.Images
{
    public class DeleteImagesOlderThanCommandHandler : IRequestHandler<DeleteImagesOlderThanCommand>
    {

        public DeleteImagesOlderThanCommandHandler()
        {
        }

        public Task Handle(DeleteImagesOlderThanCommand request, CancellationToken cancellationToken = default)
        {

          

            return Task.CompletedTask;

        }
    }
}
