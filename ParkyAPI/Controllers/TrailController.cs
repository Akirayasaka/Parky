using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrailController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrailController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// 取得Trail資料清單
        /// 
        /// Postman Body: 不需要
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetTrails()
        {
            var objList = _unitOfWork.Trail.GetAll(includeProperties: "NationalPark");
            var objDto = new List<TrailDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<TrailDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// 取單筆資料
        /// 
        /// Postman Body: 不需要
        /// </summary>
        /// <param name="trailId"></param>
        /// <returns></returns>
        [HttpGet("{trailId:int}", Name = "GetTrail")]
        public IActionResult GetTrail(int trailId)
        {
            var obj = _unitOfWork.Trail.GetFirstOrDefault(filter: x=> x.Id == trailId, includeProperties: "NationalPark");
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<TrailDto>(obj);
            return Ok(objDto);
        }

        [HttpGet("[action]/{nationalParkId:int}")]
        public IActionResult GetTrailInNationalPark(int nationalParkId)
        {
            var objList = _unitOfWork.Trail.GetTrailsInNationalPark(nationalParkId);
            //var objList2 = _unitOfWork.Trail.GetAll(filter: x=> x.NationalParkId == nationalParkId, includeProperties: "NationalPark");
            if(objList == null)
            {
                return NotFound();
            }
            var objDto = new List<TrailDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<TrailDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// 新增資料
        /// 
        /// Postman Body: raw & JSON
        /// </summary>
        /// <param name="trailDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateTrail([FromBody] TrailDto trailDto)
        {
            if(trailDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_unitOfWork.Trail.TrailExists(trailDto.Name))
            {
                ModelState.AddModelError("", "Trail Exists!");
                return StatusCode(404, ModelState);
            }
            var trailObj = _mapper.Map<Trail>(trailDto);
            try
            {
                _unitOfWork.Trail.Add(trailObj);
                _unitOfWork.Save();
                return Ok();
            }catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error occurred during creating data {trailObj.Name}");
                return StatusCode(500, ModelState);
            }
        }

        /// <summary>
        /// 更新資料
        /// 
        /// Postman Body: raw & JSON
        /// </summary>
        /// <param name="trailId"></param>
        /// <param name="trailDto"></param>
        /// <returns></returns>
        [HttpPatch("{trailId:int}", Name = "UpdateTrail")]
        public IActionResult UpdateTrail(int trailId, [FromBody] TrailDto trailDto)
        {
            if(trailDto == null || trailId != trailDto.Id)
            {
                return BadRequest(ModelState);
            }

            var trailObj = _mapper.Map<Trail>(trailDto);
            try
            {
                _unitOfWork.Trail.UpdateTrail(trailObj);
                _unitOfWork.Save();
                return Ok();
            }catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error occurred during updating data {trailObj.Name}");
                return StatusCode(500, ModelState);
            }
        }

        /// <summary>
        /// 刪除單筆資料
        /// 
        /// Postman Body: 不需要
        /// </summary>
        /// <param name="trailId"></param>
        /// <returns></returns>
        [HttpDelete("{trailId:int}", Name = "DeleteTrail")]
        public IActionResult DeleteTrail(int trailId)
        {
            if (_unitOfWork.Trail.TrailExists(trailId))
            {
                return NotFound();
            }

            var trailObj = _unitOfWork.Trail.GetFirstOrDefault(filter: x => x.Id == trailId, includeProperties: "NationalPark");
            try
            {
                _unitOfWork.Trail.Remove(trailObj);
                _unitOfWork.Save();
                return Ok();
            }catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error occurred during deleting data {trailObj.Name}");
                return StatusCode(500, ModelState);
            }
        }
    }
}
