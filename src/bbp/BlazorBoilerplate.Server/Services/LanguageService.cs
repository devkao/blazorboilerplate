using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorBoilerplate.Server.Middleware.Wrappers;
using BlazorBoilerplate.Shared.Dto.CUSTOMIZED;
using AutoMapper;
using BlazorBoilerplate.Server.Data;
using BlazorBoilerplate.Server.Models;
using BlazorBoilerplate.Shared.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace BlazorBoilerplate.Server.Services
{
    public interface ILanguageService
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(long id);
        Task<ApiResponse> Create(Language todo);
        Task<ApiResponse> Update(Language todo);
        Task<ApiResponse> Delete(long id);
    }

    public class LanguageService : ILanguageService
    {
        private readonly DebugDatacontext _db;
        private readonly IMapper _autoMapper;

        public LanguageService(DebugDatacontext db, IMapper autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }





        public async Task<ApiResponse> Get()
        {
            try
            {
               
                return new ApiResponse(200, "Retrieved Language", _autoMapper.ProjectTo<Language>(_db.Languages).ToList());
            }
            catch (Exception ex)
            {
                return new ApiResponse(400, ex.Message);
            }
        }

        public async Task<ApiResponse> Get(long id)
        {
            Language todo = _db.Languages.FirstOrDefault(t => t.Id == id);
            if (todo != null)
            {
                return new ApiResponse(200, "Retrived Todo", _autoMapper.Map<Language>(todo));
            }
            else
            {
                return new ApiResponse(400, "Failed to Retrieve Todo");
            }
        }

        public async Task<ApiResponse> Create(Language todoDto)
        {
            Language todo = _autoMapper.Map<Language, Language>(todoDto);
            await _db.Languages.AddAsync(todo);
            await _db.SaveChangesAsync();

            return new ApiResponse(200, "Created Todo", todo);
        }

        public async Task<ApiResponse> Update(Language todoDto)
        {
            Language todo = _db.Languages.FirstOrDefault(t => t.Id == todoDto.Id);
            if (todo != null)
            {               

                _autoMapper.Map<Language, Language>(todoDto, todo);
                await _db.SaveChangesAsync();
                return new ApiResponse(200, "Updated Todo", todo);
            }
            else
            {
                return new ApiResponse(400, "Failed to update Todo");
            }
        }

        public async Task<ApiResponse> Delete(long id)
        {
            Language todo = _db.Languages.FirstOrDefault(t => t.Id == id);
            if (todo != null)
            {
                _db.Languages.Remove(todo);
                await _db.SaveChangesAsync();
                return new ApiResponse(200, "Soft Delete Todo");
            }
            else
            {
                return new ApiResponse(400, "Failed to update Todo");
            }
        }







    }




}




