using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.ViewModels
{
    public class ProductImageVM
    {
        public Guid ProductId { get; set; }
        public IFormFile? Image { get; set; }
    }
}
