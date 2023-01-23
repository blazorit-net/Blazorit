using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Products {
    public class PictureLinkPart {   
        public string LinkPart { get; set; } = null!;

        public string PicSize { get; set; } = null!;

        public short OrderNum { get; set; }       
    }
}
