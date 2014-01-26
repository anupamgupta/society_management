using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SocietyManager.Core.Attributes
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        public CustomAuthorize(params string[] roles)
        {
            this.Roles = String.Join(", ", roles);
        }
    }

    public class EmailPatternAttribute : RegularExpressionAttribute
    {
        private const string EmailPattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        public EmailPatternAttribute()
            : base(EmailPattern)
        {
            ErrorMessageResourceType = typeof(Strings);
            ErrorMessageResourceName = "Validation_InvalidEmail";
        }
    }

    public class UrlPatternAttribute : RegularExpressionAttribute
    {
        private const string UrlPattern = @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$";
        public UrlPatternAttribute()
            : base(UrlPattern)
        {
            ErrorMessageResourceType = typeof(Strings);
            ErrorMessageResourceName = "Validation_InvalidUrl";
        }
    }

    public class CustomRequiredAttribute : RequiredAttribute
    {
        public CustomRequiredAttribute()
        {
            ErrorMessageResourceType = typeof(Strings);
            ErrorMessageResourceName = "Required";
        }
    }
}
