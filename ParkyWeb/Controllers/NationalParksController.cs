using Microsoft.AspNetCore.Mvc;
using ParkyWeb.Repository.IRepository;

namespace ParkyWeb.Controllers
{
    public class NationalParksController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public NationalParksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllNationalPark()
        {
            return Json(new { data = await _unitOfWork.NationalPark.GetAllAsync(SD.NationalParkAPIPath)});
        }
    }
}
