﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParkController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NationalParkController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// 取得NationalPark列表
        /// Postman Url: https://localhost:7063/api/nationalPark
        /// Postman Body: 不需要
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var objList = _unitOfWork.NationalPark.GetAll();
            var objDto = new List<NationalParkDto>();

            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<NationalParkDto>(obj));
            }

            // 透過Dto中介, 避免直接存取model, 降低耦合
            return Ok(objDto);
        }

        /// <summary>
        /// 取單筆資料
        /// Postman Url: https://localhost:7063/api/nationalPark/3
        /// Postman Body: 不需要
        /// </summary>
        /// <param name="nationalParkId"></param>
        /// <returns></returns>
        [HttpGet("{nationalParkId:int}", Name = "GetNationalPark")]
        public IActionResult GetNationalPark(int nationalParkId)
        {
            var obj = _unitOfWork.NationalPark.Get(nationalParkId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<NationalParkDto>(obj);
            return Ok(objDto);
        }

        /// <summary>
        /// 新增資料
        /// Postman Url: https://localhost:7063/api/nationalPark
        /// Postman Body: raw & JSON
        /// </summary>
        /// <param name="nationalParkDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateNationalPart([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null)
            {
                return BadRequest(ModelState);
            }

            #region 確認名稱是否重複
            if (_unitOfWork.NationalPark.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "National Park Exists!");
                return StatusCode(404, ModelState);
            }
            #endregion

            var obj = _mapper.Map<NationalPark>(nationalParkDto);

            try
            {
                _unitOfWork.NationalPark.Add(obj);
                _unitOfWork.Save();

                #region return status 200
                return Ok();
                #endregion

                #region return status 201
                //return CreatedAtRoute("GetNationalPark", new { nationalParkId = obj.Id}, obj);
                #endregion
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error occurred during creating data {obj.Name}");
                return StatusCode(500, ModelState);
            }
        }
    }
}
