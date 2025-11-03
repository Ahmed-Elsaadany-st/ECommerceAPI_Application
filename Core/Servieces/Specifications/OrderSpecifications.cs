using Domain.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servieces.Specifications
{
    public class OrderSpecifications : BaseSpecifications<Order,Guid>
    {
        //Get All Orders With Email
        public OrderSpecifications(string Email) :base(o=>o.UserEmail==Email)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            AddOrderByDesceding(O => O.OrderDate);

        }
        //Get Order by Id
        public OrderSpecifications(Guid Id):base(o=>o.Id==Id) 
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
        }
    }
}
