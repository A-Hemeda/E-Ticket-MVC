using AutoMapper;
using eTickets.BLL.Interfaces;
using eTickets.BLL.Repositories;
using eTickets.DAL.Entities;
using eTickets.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.PL.Controllers
{
    public class CinemaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CinemaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var cinema = await _unitOfWork.CinemaRepository.GetAllAsync();
            return View(cinema);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CinemaViewModel cinemaVM)
        {
            if (ModelState.IsValid)
            {

                var cinema = _mapper.Map<Cinema>(cinemaVM);
                await _unitOfWork.CinemaRepository.AddAsync(cinema);
                return RedirectToAction("Index");
            }
            return View(cinemaVM);
        }

        public async Task<IActionResult> Details(int id, string viewName = "Details")
        {
            if (id == 0)
                return NotFound();
            var cinema = await _unitOfWork.CinemaRepository.GetByIdAsync(id);
            var mappeddCinema = _mapper.Map<CinemaViewModel>(cinema);

            if (cinema is null)
                return NotFound();

            return View(viewName, mappeddCinema);
        }

        public async Task<IActionResult> Update(int id)
        {
            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, CinemaViewModel cinemaVM)
        {
            if (id != cinemaVM.Id)
                return NotFound();

            try
            {
                if (ModelState.IsValid)
                {
                    var cinema = _mapper.Map<Cinema>(cinemaVM);
                    _unitOfWork.CinemaRepository.Update(cinema);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return View(cinemaVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return NotFound();

            var cinema = await _unitOfWork.CinemaRepository.GetByIdAsync(id);
            _mapper.Map<CinemaViewModel>(cinema);

            if (cinema is null)
                return NotFound();

            await _unitOfWork.CinemaRepository.DeleteAsync(cinema);

            return RedirectToAction("Index");
        }
    }
}
