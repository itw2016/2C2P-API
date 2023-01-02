using Dapper.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Dapper.WebApi.Handlers
{
    public interface IExtractHandler
    {
        List<Trans> LoadCsvData(IFormFile file);
        List<Trans> LoadXmlData(IFormFile file);
    }
}
