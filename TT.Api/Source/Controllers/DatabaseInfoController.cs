using Microsoft.AspNetCore.Mvc;
using TT.Services.Interafces;

namespace TT.Source.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DatabaseInfoController
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
}
