using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;

namespace Application.Dtos
{
    public class CsvProductDtoMapping : CsvMapping<ProductDto>
    {
        public CsvProductDtoMapping() :base()
        {
            MapProperty(0, x => x.Name);
            MapProperty(1, x => x.Protein);
            MapProperty(2, x => x.Fat);
            MapProperty(3, x => x.Carbo);
        }
    }
}
