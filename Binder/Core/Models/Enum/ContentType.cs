using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Core.Models.Enum
{
    public enum ContentType
    {
        Redirect, //redirects to different URL
        Download, //downloads site and replaces content
        Replace   //replaces content with whats specified in rule
    }
}
