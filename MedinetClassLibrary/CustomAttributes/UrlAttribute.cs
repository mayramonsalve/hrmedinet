using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class UrlAttribute : ValidationAttribute
{
    public string Property { get; set; }

    public UrlAttribute()
    { }

    public override bool IsValid(Object Value)
    {
        if (Value == null) { return true; }

        var myUrl = Value.ToString();

        Regex myRegExp = new Regex(@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$");

        return myRegExp.IsMatch(myUrl);
    }

}

public class UrlValidator : DataAnnotationsModelValidator<UrlAttribute>
{
    string property;

    public UrlValidator(ModelMetadata metadata, ControllerContext context, UrlAttribute attribute)
        : base(metadata, context, attribute)
    {
        property = attribute.Property;
    }

    public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
    {
        var rule = new ModelClientValidationRule
        {
            ErrorMessage = Attribute.ErrorMessage,
            ValidationType = "UrlValidator"
        };

        rule.ValidationParameters.Add("propertyname", property);

        return new[] { rule };
    }
}