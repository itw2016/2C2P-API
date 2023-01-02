using Dapper.WebApi.Handlers;
using Dapper.WebApi.Models;
using Dapper.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;

namespace Dapper.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransController : ControllerBase
    {
        private readonly ITransRepository _transRepository;
        private readonly IExtractHandler _extractHandler;

        public TransController(ITransRepository transRepository, IExtractHandler extractHandler)
        {
            _transRepository = transRepository;
            _extractHandler = extractHandler;
        }


        [Microsoft.AspNetCore.Mvc.HttpGet()]
        [Route("byCurrencyCode/{currencyCode}")]
        public async Task<string> GetByCurrencyCode(string currencyCode)
        {
            var trans = await _transRepository.GetByCurrencyCode(currencyCode);
            string result = JsonSerializer.Serialize(trans);
            return result;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet()]
        [Route("byDateRange/{startDate}/{endDate}")]
        public async Task<string> GetByDateRange(string startDate, string endDate)
        {
            var trans = await _transRepository.GetByDateRange(startDate, endDate);
            string result = JsonSerializer.Serialize(trans);
            return result;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet()]
        [Route("byStatus/{status}")]
        public async Task<string> GetByStatus(string status)
        {
            var trans = await _transRepository.GetByStatus(status.Trim());
            string result = JsonSerializer.Serialize(trans);
            return result;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<string> GellAll()
        {
            var trans = await _transRepository.GetAllTrans();
            List<Trans> tranList = new List<Trans>();
            foreach(var item in trans)
            {
                Trans t = new Trans();
                t.TransactionId= item.TransactionId;
                t.TransactionDate = item.TransactionDate;
                t.Amount= item.Amount;
                t.CurrencyCode= item.CurrencyCode;
                if (item.Status.Trim().ToUpper().Equals("APPROVED"))
                {
                    t.Status = "A";
                }
                else if (item.Status.Trim().ToUpper().Equals("FAILED") || item.Status.Trim().ToUpper().Equals("REJECTED"))
                {
                    t.Status = "R";
                }
                else if (item.Status.Trim().ToUpper().Equals("FINISHED") || item.Status.Trim().ToUpper().Equals("DONE"))
                {
                    t.Status = "D";
                }
                tranList.Add(t);
                
            }
            string result = JsonSerializer.Serialize(tranList);
            return result;
        }

        
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Trans>> GetById(int id)
        {
            var trans = await _transRepository.GetById(id);
            return Ok(trans);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IActionResult> SubmitPost([FromForm] IFormFile file)
        {
            try
            {
                if (file.ContentType.Equals("text/csv"))
                {
                    var transList = _extractHandler.LoadCsvData(file);
                    foreach (var entity in transList)
                    {
                        await _transRepository.AddTrans(entity);
                    }
                    return Ok();
                }
                else if (file.ContentType.Equals("text/xml"))
                {
                    var transList = _extractHandler.LoadXmlData(file);
                    foreach (var entity in transList)
                    {
                        await _transRepository.AddTrans(entity);
                    }
                    return Ok();
                }
                else
                {
                    return BadRequest("File type not supported");
                }
            }
            catch(Exception ex)
            {
                return BadRequest($"Error:{ex.Message}");
            }
            
            
        }


        /*[HttpPost]
        public async Task<ActionResult> AddTrans(Trans entity)
        {
            await _transRepository.AddTrans(entity);
            return Ok(entity);
        }*/

        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public async Task<ActionResult<Trans>> Update(Trans entity, int id)
        {
            await _transRepository.UpdateTrans(entity, id);
            return Ok(entity);
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _transRepository.RemoveTrans(id);
            return Ok();
        }
    }
}