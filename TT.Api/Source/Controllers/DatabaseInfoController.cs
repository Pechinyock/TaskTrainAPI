using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TT.Api.Services;

namespace TT.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DatabaseInfoController : ControllerBase
{
    private readonly IDatabaseInfoService _databaseInfoService;

    public DatabaseInfoController(IDatabaseInfoService databaseInfoService)
    {
        _databaseInfoService = databaseInfoService;
    }

    [HttpGet]
    public string GetDatabaseVendorName()
    {
        return _databaseInfoService.GetDatabaseVendorName();
    }

    [HttpGet]
    [Authorize]
    public string GetDatabaseVersion() 
    {
        return "sdf";
    }
}
