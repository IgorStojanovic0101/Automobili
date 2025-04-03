using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.DataModels;

namespace Template.Application.Utilities
{
    public static class IncludeEnities
    {


        public static class All_Users
        {
           

        }

        public static class ExpressionHelper
        {
            public static string GetMemberName<T, TProperty>(Expression<Func<T, TProperty>> expression)
            {
                if (expression.Body is MemberExpression memberExpression)
                {
                    return memberExpression.Member.Name;
                }

                throw new ArgumentException("Expression is not a member access", nameof(expression));
            }


        }

    }



}
