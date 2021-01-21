using System;
using System.ComponentModel.DataAnnotations;

public class LoginAttribute : RegularExpressionAttribute
{
    public LoginAttribute()
        : base(@"^[a-zA-Z0-9_.]{3,16}$") { }
}