using LMS.BookInventory.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMS.BookInventory.Controllers;

[ApiController]
[Route("api/v{version:ApiVersions.V1}/[controller]")]
public class BooksController: ControllerBase
{
    public BooksController(){}

    public ActionResult Ping()
    {
        return Ok("Pong");
    }
}