using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Application.Queries.Images
{
    public class DeleteImagesOlderThanQuery : IRequest<int>
    {
        public string Symbol { get; }
        public DateTime CutoffDate { get; }

        public DeleteImagesOlderThanQuery(string symbol, DateTime cutoffDate)
        {
            Symbol = symbol;
            CutoffDate = cutoffDate;
        }
    }
}
