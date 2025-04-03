using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;    
using MediatR;


namespace Template.Domain.Events
{

    public class OrderPlacedEvent : INotification
    {
        public int OrderId { get; }
        public DateTime OrderDate { get; }

        public OrderPlacedEvent(int orderId, DateTime orderDate)
        {
            OrderId = orderId;
            OrderDate = orderDate;
        }
    }
}
