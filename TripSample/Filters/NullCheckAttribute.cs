using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

public class NullCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var argument in context.ActionArguments)
        {
            var paramName = argument.Key;
            var paramValue = argument.Value;

            if (paramValue == null)
            {
                SetError(context, $"Parametre '{paramName}' null olamaz.");
                return;
            }

            var props = paramValue.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                if (!prop.PropertyType.IsValueType && prop.PropertyType != typeof(string))
                    continue;

                var value = prop.GetValue(paramValue);

                if (value == null || (prop.PropertyType == typeof(string) && string.IsNullOrWhiteSpace(value.ToString())))
                {
                    var inputName = string.Empty;
                    if(prop.Name == "DepartureDate")
                    {
                        inputName = "Sefer Tarihi";
                    }
                    else if (prop.Name == "OriginId" || prop.Name == "OriginName")
                    {
                        inputName = "Nereden Bilgisi";
                    }
                    else if (prop.Name == "TargetId" || prop.Name == "TargetName")
                    {
                        inputName = "Nereye Bilgisi";
                    }

                    SetError(context, $"{inputName} bos olamaz.");
                    return;
                }
            }
        }
    }

    private void SetError(ActionExecutingContext context, string message)
    {
        var controller = (Controller)context.Controller;
        controller.TempData["ErrorMessage"] = message;

        context.Result = controller.RedirectToAction("Index", "Home");
    }
}
