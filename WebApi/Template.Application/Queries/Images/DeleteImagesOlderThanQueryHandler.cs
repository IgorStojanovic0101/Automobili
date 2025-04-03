using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Application.Queries.Images
{
    public class DeleteImagesOlderThanQueryHandler : IRequestHandler<DeleteImagesOlderThanQuery, int>
    {
        public DeleteImagesOlderThanQueryHandler()
        {
        }

        public Task<int> Handle(DeleteImagesOlderThanQuery request, CancellationToken cancellationToken)
        {
            // Your logic for handling the request goes here
            return Task.FromResult(1);
        }
    }
}
