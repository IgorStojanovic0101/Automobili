using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Application.Commands.Images
{
    public class DeleteImagesOlderThanCommand : IRequest
    {
        public string Symbol { get; }
        public DateTime CutoffDate { get; }

        public DeleteImagesOlderThanCommand(string symbol, DateTime cutoffDate)
        {
            Symbol = symbol;
            CutoffDate = cutoffDate;
        }
    }
}
