using AutoMapper;
using eTickets.BLL.Interfaces;
using eTickets.BLL.Repositories;
using eTickets.DAL.Entities;
using eTickets.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.PL.Controllers
{
    public class ProducerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProducerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var producer = await _unitOfWork.ProducerRepository.GetAllAsync();
            return View(producer);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProducerViewModel producerVM)
        {
            if (ModelState.IsValid)
            {

                var producer = _mapper.Map<Producer>(producerVM);
                await _unitOfWork.ProducerRepository.AddAsync(producer);
                return RedirectToAction("Index");
            }
            return View(producerVM);
        }

        public async Task<IActionResult> Details(int id, string viewName = "Details")
        {
            if (id == 0)
                return NotFound();
            var producer = await _unitOfWork.ProducerRepository.GetByIdAsync(id);
            var mappeddProducer = _mapper.Map<ProducerViewModel>(producer);

            if (producer is null)
                return NotFound();

            return View(viewName, mappeddProducer);
        }

        public async Task<IActionResult> Update(int id)
        {
            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, ProducerViewModel producerVM)
        {
            if (id != producerVM.Id)
                return NotFound();

            try
            {
                if (ModelState.IsValid)
                {
                    var producer = _mapper.Map<Producer>(producerVM);
                    _unitOfWork.ProducerRepository.Update(producer);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return View(producerVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return NotFound();

            var producer = await _unitOfWork.ProducerRepository.GetByIdAsync(id);
            _mapper.Map<ProducerViewModel>(producer);

            if (producer is null)
                return NotFound();

            await _unitOfWork.ProducerRepository.DeleteAsync(producer);

            return RedirectToAction("Index");
        }
    }
}
