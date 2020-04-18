using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yezhanbafang.fw.winform.classControlForm
{
    interface PropertyInterface
    {
        string PropertyName { get; set; }
        string PropertyValue { get; set; }
        string PropertyType { get; set; }
    }
}
