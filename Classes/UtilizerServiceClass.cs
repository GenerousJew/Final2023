using UtilizerAPI.Models;

namespace UtilizerAPI.Classes
{
    public class UtilizerServiceClass
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public int? Progress { get; set; }
        public string StatusName { get; set; }
        public decimal? Result { get; set; }

        public UtilizerServiceClass(OrderService orderService)
        {
            Id = orderService.Id;
            Code = orderService.Service;
            Name = orderService.ServiceNavigation.Name;
            Progress = orderService.PercentProgress;
            StatusName = orderService.StatusNavigation.Name;
            Result = orderService.Result.Result1;
        }
    }
}
