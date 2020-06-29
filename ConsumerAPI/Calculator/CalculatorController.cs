﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;

namespace ConsumerAPI.Calculator
{
    [Route("api/v0/")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpGet("calculajuros")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<double> CalculaJuros()
        {
            try
            {
                var result = 666;

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}