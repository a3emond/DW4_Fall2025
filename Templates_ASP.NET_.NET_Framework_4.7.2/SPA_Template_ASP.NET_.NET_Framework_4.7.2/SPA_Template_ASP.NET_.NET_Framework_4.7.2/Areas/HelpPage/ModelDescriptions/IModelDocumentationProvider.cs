using System;
using System.Reflection;

namespace SPA_Template_ASP.NET_.NET_Framework_4._7._2.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}