﻿using AutoMapper;
using Core.Exceptions;
using Core.Models.Users.DTOs;
using Core.Models.Users;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerAPI.Calculator
{
    [Route("api/v0/[controller]")]
    [ApiController]
    [Authorize (Roles = "AuthorizedUser")]
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