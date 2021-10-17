using System;

namespace Support_Chat_App.Repositories.Authorization
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AnonymousAttribute : Attribute
    {

    }
}
