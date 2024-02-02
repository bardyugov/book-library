using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BookLibrary.Core.Controllers;

public class BaseController : ControllerBase
{
    public IActionResult ConvertToActionResult<T>(Result<T> result)
    {
        if (result.IsFailed)
        {
            throw new BadHttpRequestException(result.Errors.Last().Message);
        }

        return Ok(result.Value);
    }
}