using FinalAPI.Models;

namespace FinalAPI.Classes
{
    public class UtilizerService
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public int? Progress { get; set; }
        public string StatusName { get; set; }
        public decimal? Result { get; set; }

        public UtilizerService(OrderService orderService) 
        {
            Id = orderService.Id;
            Code = orderService.Service;
            Name = orderService.ServiceNavigation.Name;
            Progress = orderService.PercentProgress;
            StatusName = orderService.StatusNavigation.Name;
            Result = orderService.Result != null ? orderService.Result.Result1 : null;
        }
    }
}
