using DevIO.Business.Interfaces;
using DevIO.Business.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DevIO.Api.V1.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    private readonly INotifier _notifier;
    public readonly IUser AppUser;

    protected Guid UserId { get; set; }
    protected bool UserAuthenticated { get; set; }

    public MainController(INotifier notifier, IUser appUser)
    {
        _notifier = notifier;
        AppUser = appUser;

        if (appUser.IsAuthenticated())
        {
            UserId = appUser.GetUserId();
            UserAuthenticated = true;
        }
    }

    protected bool IsOperationValid()
    {
        return !_notifier.HaveNotification();
    }

    protected ActionResult CustomResponse(object? result = null)
    {
        if (IsOperationValid())
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        return BadRequest(new
        {
            success = false,
            errors = _notifier.GetNotifications().Select(n => n.Message)
        });
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid)
            NotificationErrorModelInvalid(modelState);

        return CustomResponse();
    }

    protected void NotificationErrorModelInvalid(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);

        foreach (var error in errors)
        {
            var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;

            NotifyError(errorMessage);
        }
    }

    protected void NotifyError(string message)
    {
        _notifier.Handle(new Notification(message));
    }
}
