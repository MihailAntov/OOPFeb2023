using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHierarchy.Models;

public abstract class StringCollection
{
    protected List<String> items;

    public StringCollection()
    {
        items = new List<String>();
    }
}
