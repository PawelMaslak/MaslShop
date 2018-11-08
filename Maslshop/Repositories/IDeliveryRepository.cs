using Maslshop.Models.Core;
using Maslshop.Models.ViewModels.Delivery;
using System.Collections.Generic;

namespace Maslshop.Repositories
{
    public interface IDeliveryRepository
    {
        IEnumerable<Delivery> GetDeliveriesOptionsList();
        void AddDeliveryType(DeliveryFormViewModel viewModel);
        Delivery GetDeliveryById(int id);
        void RemoveDeliveryType(int id);
    }
}