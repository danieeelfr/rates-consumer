﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Exceptions;
using Core.Models.Users;
using Core.Models.Users.DTOs;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerAPI.ShowMe {
    [Route ("api/v0/[controller]")]
    [ApiController]
    // [Authorize (Roles = "AuthorizedUser")]
    public class ShowMeController : ControllerBase {
        [HttpGet ("showmethecode")]
        [ProducesResponseType (StatusCodes.Status200OK)]
        public ActionResult<List<string>> ShowMetTheCode () {
            var urls = new [] {
                new {
                    title = "API 1",
                    api_url = "https://danielfr-softplan-rates-api.azurewebsites.net/api/v0/{controller}/{action}",
                    api_doc = "https://danielfr-softplan-rates-api.azurewebsites.net/index.html",
                    repository_url = "https://github.com/danieeelfr/rates-service"     
                },
                new {
                    title = "API 2",
                    api_url = "",
                    api_doc = "",
                    repository_url = "https://github.com/danieeelfr/rates-consumer"     
                }
            };

            return Ok (urls);

        }
    }
}