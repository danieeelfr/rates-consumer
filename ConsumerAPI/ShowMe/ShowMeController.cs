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
                new {
                    title = "API 1",
                    api_doc = "https://danielfr-softplan-rates-api.azurewebsites.net/index.html",
                    repository_url = "https://github.com/danieeelfr/rates-service"     
                },
                new {
                    title = "API 2",
                    api_doc = "https://danielfr-softplan-consumer-api.azurewebsites.net/index.html",
                    repository_url = "https://github.com/danieeelfr/rates-consumer"     
                }
            };

            return Ok (urls);

        }
    }
}