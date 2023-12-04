using API.Models;

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
        public string IsCanApprove
        { 
            get
            {
                if (StatusName == "Завершено")
                {
                    return "Visible";
                }
                else
                {
                    return "Hidden";
                }
            }
        }
        public string IsCanSend
        {
            get
            {
                if (StatusName == "Не начато" || StatusName == "Требуется повторная утилизация" || StatusName == "Начато")
                {
                    return "Visible";
                }
                else
                {
                    return "Hidden";
                }
            }
        }
    }
}
