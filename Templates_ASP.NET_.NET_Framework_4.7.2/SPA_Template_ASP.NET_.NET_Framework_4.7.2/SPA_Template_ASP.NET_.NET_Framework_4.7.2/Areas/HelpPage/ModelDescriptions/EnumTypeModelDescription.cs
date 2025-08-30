using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SPA_Template_ASP.NET_.NET_Framework_4._7._2.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}