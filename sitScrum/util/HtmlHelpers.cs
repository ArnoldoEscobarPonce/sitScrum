using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace sitScrum.util
{
    public static class HtmlHelpers
    {
        public static string DisplayShortNameFor<TModel, TValue>(this IHtmlHelper<global::System.Collections.Generic.IEnumerable<TModel>> t, global::System.Linq.Expressions.Expression<global::System.Func<TModel, TValue>> exp)
        {
            CustomAttributeNamedArgument? DisplayName = null;
            var prop = exp.Body as MemberExpression;
            if (prop != null)
            {
                var DisplayAttrib = (from c in prop.Member.GetCustomAttributesData()
                                     where c.AttributeType == typeof(DisplayAttribute)
                                     select c).FirstOrDefault();
                if (DisplayAttrib != null)
                {
                    DisplayName = DisplayAttrib.NamedArguments.FirstOrDefault(d => d.MemberName == "ShortName");

                    if (DisplayName.Value.TypedValue.Value == null)
                    {
                        DisplayName = DisplayAttrib.NamedArguments.FirstOrDefault(d => d.MemberName == "Name");
                    }


                    if (DisplayName != null && DisplayName.Value.TypedValue.Value != null)
                    {
                        return DisplayName.Value.TypedValue.Value.ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return prop.Member.Name;
                }
            }
            return "";

        }

    }
}