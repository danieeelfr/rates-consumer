﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerAPI.ShowMe
{
    [Route ("api/v0")]
    [ApiController]
    public class ShowMeController : ControllerBase {
        [HttpGet ("showmethecode")]
        [ProducesResponseType (StatusCodes.Status200OK)]
        public ActionResult<List<string>> ShowMetTheCode () {
            var urls = new [] {
                "https://danielfr-softplan-rates-api.azurewebsites.net/index.html",
                "https://danielfr-softplan-consumer-api.azurewebsites.net/index.html"  
                };

            return new JsonResult(new {urls = urls});

        }
    }
}