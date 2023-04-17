using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Domain.Exceptions
{
    public class ElementNotUniqueException : Exception
    {
        public ElementNotUniqueException(string entity) : base($@"{entity} already exists.")
        {

        }
    }
}
