using System.ComponentModel.DataAnnotations;

namespace BGLibrary.Models
{
    public class RequestBuilderViewModel
    {
        [Required]
        public List<SelectViewModel> SelectColumns { get; set; } = new List<SelectViewModel>() { };
        public List<OrderByViewModel> Orderby { get; set; } = new List<OrderByViewModel>() { };
        public List<WhereViewModel> Where { get; set; } = new List<WhereViewModel>() { };
    }
}