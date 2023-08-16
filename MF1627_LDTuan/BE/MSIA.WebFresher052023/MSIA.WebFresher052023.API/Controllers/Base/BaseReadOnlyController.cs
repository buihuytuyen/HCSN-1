﻿using AutoMapper;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSIA.WebFresher052023.Application.Interface.Base;
using MSIA.WebFresher052023.Domain.Resource;
using MSIA.WebFresher052023.Domain.Result;
using MySqlX.XDevAPI.Common;

namespace MSIA.WebFresher052023.API.Controllers.Base
{
    public abstract class BaseReadOnlyController<TEntity, TModel, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : ControllerBase
    {
        #region Fields
        protected readonly IBaseService<TEntity, TModel, TEntityDto, TEntityCreateDto, TEntityUpdateDto> _baseService;
        protected readonly IMapper _mapper;
        #endregion

        #region Constructor
        protected BaseReadOnlyController(IBaseService<TEntity, TModel, TEntityDto, TEntityCreateDto, TEntityUpdateDto> baseService, IMapper mapper)
        {
            _baseService = baseService;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        [HttpGet("GetNewCode")]
        public virtual async Task<string> GetNewCode()
        {
            var code = await _baseService.GetNewCode();
            return code;
        }

        /// <summary>
        /// Api lấy 1 bản ghi theo mã code
        /// </summary>
        /// <param name="code">Mã code của bản ghi cần tìm</param>
        /// <returns>Một bản ghi</returns>
        /// Created by: ldtuan (18/07/2023)
        [HttpGet("{code}")]
        public virtual async Task<IActionResult> GetAsync([FromRoute] string code)
        {
            var asset = await _baseService.GetAsync(code);
            if (asset == null)
            {
                var error = new OperationResult { UserMessage = ErrorMessages.Conflict, ErrorCode=10000 };
                return StatusCode(StatusCodes.Status400BadRequest, error);
            }
            else
            {
                return StatusCode(StatusCodes.Status201Created, asset);
            }
        }
        /// <summary>
        /// Api thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Dữ liệu của bản ghi muốn thêm mới</param>
        /// <returns>True hoặc false tương ứng với thêm mới thành công hay thất bại</returns>
        /// Created by: ldtuan (18/07/2023)
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TEntityCreateDto entityCreateDto)
        {
            var result = await _baseService.InsertAsync(entityCreateDto);
            if (result)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                var error = new OperationResult { UserMessage = ErrorMessages.Other };
                return StatusCode(StatusCodes.Status400BadRequest, error);
            }
        }

        /// <summary>
        /// Api cập nhật 1 bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi muốn cập nhật</param>
        /// <param name="assetUpdateDto">Dữ liệu của bản ghi muốn cập nhật</param>
        /// <returns>True hoặc false tương ứng với cập nhật thành công hay thất bại</returns>
        /// Created by: ldtuan (18/07/2023)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] TEntityUpdateDto entityUpdateDto)
        {
            var result = await _baseService.UpdateAsync(id, entityUpdateDto);
            if (result)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                var error = new OperationResult { UserMessage = ErrorMessages.Other };
                return StatusCode(StatusCodes.Status400BadRequest, error);
            }
        }

        /// <summary>
        /// Api xóa 1 bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi muốn xóa</param>
        /// <returns>True hoặc false tương ứng với xóa thành công hay thất bại</returns>
        /// Created by: ldtuan (18/07/2023)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id)
        {
            var result = await _baseService.DeleteAsync(id);

            if (result)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                var error = new OperationResult { UserMessage = ErrorMessages.Other };
                return StatusCode(StatusCodes.Status400BadRequest, error);
            }
        }

        /// <summary>
        /// Api xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">Id của các bản ghi muốn xóa</param>
        /// <returns>True hoặc false tương ứng với xóa thành công hay thất bại</returns>
        /// Created by: ldtuan (18/07/2023)
        [HttpDelete]
        public async Task<IActionResult> DeleteManyByIdAsync([FromBody] List<Guid> ids)
        {
            var result = await _baseService.DeleteManyAsync(ids);
            if (result)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                var error = new OperationResult { UserMessage = ErrorMessages.Other };
                return StatusCode(StatusCodes.Status400BadRequest, error);
            }
        }

        /// <summary>
        /// Lấy tổng số bản ghi
        /// </summary>
        /// <returns>tổng số bản ghi dạng int</returns>
        /// Created by: ldtuan (28/07/2023)
        [HttpGet("Count")]
        public async Task<int> GetCountAsync()
        {
            var count = await _baseService.GetCountAsync();
            // Khi trả về 1 giá trị kiểu nguyên thủy trong 1 phương thức của api thì 
            // giá trị trả về sẽ tự động được đóng gói thành ActionResult
            // với mã trạng thái là HTTP 200 OK
            return count;
        }
        #endregion
    }
}
