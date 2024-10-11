using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PhoneList2.Data;
using PhoneList2.Models;

namespace PhoneList2.Controllers
{
    public class PhoneController : Controller
    {
        private readonly ApplicationDBContext _dbContext;

        public PhoneController(ApplicationDBContext db)
        {
            _dbContext = db;
        }
        public IActionResult Index()
        {
            var contacts = _dbContext.phoneLists
                 .Include(pl => pl.Numbers)  // Eagerly load the Numbers collection
                 .ToList();
            return View(contacts);
        }
        //get
        public IActionResult Create()
        {
            var phoneList = new PhoneList
            {
                Numbers = new List<PhoneNumber>()
                 {
                new PhoneNumber() 
                 }
            };
            return View(phoneList);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhoneList obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.phoneLists.Add(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _dbContext.phoneLists
                .Include(pl => pl.Numbers)  // Eagerly load the Numbers collection
                .FirstOrDefault(pl => pl.ID == id);  // Fetch the entity using FirstOrDefault

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PhoneList obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.phoneLists.Update(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _dbContext.phoneLists
                .Include(pl => pl.Numbers)  
                .FirstOrDefault(pl => pl.ID == id);  // Fetch the entity using FirstOrDefault

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
                var obj = _dbContext.phoneLists
                .Include(pl => pl.Numbers)
                .FirstOrDefault(pl => pl.ID == id);
            if (obj == null) { 
                    return NotFound();
                    }
            if (obj.Numbers != null || obj.Numbers.Any()) {
                _dbContext.phoneNumbers.RemoveRange(obj.Numbers);
            }
                _dbContext.phoneLists.Remove(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            
           
        }
    }
}
