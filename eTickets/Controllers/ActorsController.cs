using AutoMapper;
using eTickets.BLL.Interfaces;
using eTickets.BLL.Repositories;
using eTickets.DAL.Context;
using eTickets.DAL.Entities;
using eTickets.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace eTickets.PL.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActorsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var actor = await _unitOfWork.ActorRepository.GetAllAsync();
            return View(actor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActorViewModel actorVM)
        {
            if (ModelState.IsValid)
            {
                
                var actor = _mapper.Map<Actor>(actorVM);
                await _unitOfWork.ActorRepository.AddAsync(actor);                
                return RedirectToAction("Index");
            }
            return View(actorVM);
        }

        public async Task<IActionResult> Details(int id, string viewName = "Details")
        {
            if (id ==0 )
                return NotFound();
            var actor = await _unitOfWork.ActorRepository.GetByIdAsync(id);
            var mappeddActor= _mapper.Map<ActorViewModel>(actor);

            if (actor is null)
                return NotFound();

            return View(viewName, mappeddActor);
        }

        public async Task<IActionResult> Update(int id)
        {
            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, ActorViewModel actorVM)
        {
            if (id != actorVM.Id)
                return NotFound();

            try
            {
                if (ModelState.IsValid)
                {
                    var actor = _mapper.Map<Actor>(actorVM);
                    _unitOfWork.ActorRepository.Update(actor);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return View(actorVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return NotFound();

            var actor = await _unitOfWork.ActorRepository.GetByIdAsync(id);
            _mapper.Map<ActorViewModel>(actor);

            if (actor is null)
                return NotFound();

            await _unitOfWork.ActorRepository.DeleteAsync(actor);

            return RedirectToAction("Index");
        }
    }
}
