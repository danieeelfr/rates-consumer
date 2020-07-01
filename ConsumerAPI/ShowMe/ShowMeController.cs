﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerAPI.ShowMe
{
    [Route("api/v0")]
    [ApiController]
    public class ShowMeController : ControllerBase
    {
        [HttpGet("showmethecode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<string>> ShowMeTheCode()
        {
            var urls = new[] {
                "https://github.com/danieeelfr/rates-service",
                "https://github.com/danieeelfr/rates-consumer"
                };

            return new JsonResult(new { urls = urls });

        }
    }
}