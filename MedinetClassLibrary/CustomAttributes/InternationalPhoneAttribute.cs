using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class InternationalPhoneAttribute : ValidationAttribute
{
    public string Property { get; set; }

    public InternationalPhoneAttribute()
    { }

    public override bool IsValid(Object Value)
    {
        if (Value == null) { return true; }

        var myPhone = Value.ToString();

        if (string.IsNullOrEmpty(myPhone)) { return true; }

        Regex isNumber = new Regex(@"^\d+$");

        bool result = isNumber.IsMatch(FormatPhone(myPhone));

        return result;
    }

    public string FormatPhone(string maskedPhone)
    {
        if (String.IsNullOrEmpty(maskedPhone))
        {
            return null;
        }
        else
        {
            char[] delimiterChars = { '+', '(', ')', '-' };
            string unmaskedPhone = null;

            if (maskedPhone.Length > 0)
            {
                string[] phoneTerms = maskedPhone.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                foreach (var term in phoneTerms)
                {
                    unmaskedPhone += term;
                }
            }

            return unmaskedPhone;
        }
    }
}

public class InternationalPhoneValidator : DataAnnotationsModelValidator<InternationalPhoneAttribute>
{
    string property;

    public InternationalPhoneValidator(ModelMetadata metadata, ControllerContext context, InternationalPhoneAttribute attribute)
        : base(metadata, context, attribute)
    {
        property = attribute.Property;
    }

    public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
    {
        var rule = new ModelClientValidationRule
        {
            ErrorMessage = ViewRes.Models.User.ContactPhoneInvalid,
            ValidationType = "InternationalPhoneValidator"
        };

        rule.ValidationParameters.Add("propertyname", property);

        return new[] { rule };
    }
}