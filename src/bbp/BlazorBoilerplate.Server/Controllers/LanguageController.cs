using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorBoilerplate.Server.Middleware.Wrappers;
using BlazorBoilerplate.Server.Services;
using BlazorBoilerplate.Shared.AuthorizationDefinitions;
using BlazorBoilerplate.Shared.Dto.CUSTOMIZED;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BlazorBoilerplate.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILogger<LanguageController> _logger;
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService, ILogger<LanguageController> logger)
        {
            _logger = logger;
            _languageService = languageService;
        }



        // GET: api/Todo
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
        {
            return await _languageService.Get();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResponse> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResponse(400, "Todo Model is Invalid");
            }
            return await _languageService.Get(id);
        }

        // POST: api/Todo
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Post([FromBody] Language todo)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResponse(400, "Todo Model is Invalid");
            }
            return await _languageService.Create(todo);
        }

        // Put: api/Todo
        [HttpPut]
        [AllowAnonymous]
        public async Task<ApiResponse> Put([FromBody] Language todo)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResponse(400, "Todo Model is Invalid");
            }
            return await _languageService.Update(todo);
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        //[Authorize(Policy = Policies.IsAdmin)]
        public async Task<ApiResponse> Delete(long id)
        {
            return await _languageService.Delete(id); // Delete from DB
        }


    }
}