using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Naturlaboratoriet.Models;
using Naturlaboratoriet.Models.ViewModels;

namespace Naturlaboratoriet.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _HostEnvironment;

        public AdminController(ApplicationDbContext context, IHostingEnvironment HostEnvironment)

        {
            _context = context;
            _HostEnvironment = HostEnvironment;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            var products = _context.tbl_NaturLab_Product.Include(c => c.Category).AsNoTracking();
            return View(await products.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.tbl_NaturLab_Product
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            PopulateCategoriesDropDownList();
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModels model)
        {

        
            if (ModelState.IsValid)
            {
                string primaryImage = UploadedFile(model.PrimaryImage);
                string secondaryImage1 = UploadedFile(model.SecondaryImage1);
                string secondaryImage2 = UploadedFile(model.SecondaryImage2);
                string secondaryImage3 = UploadedFile(model.SecondaryImage3);

                Product product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    CategoryID = model.CategoryID,
                    PrimaryImage = primaryImage,
                    SecondaryImage1 = secondaryImage2,
                    SecondaryImage2 = secondaryImage2,
                    SecondaryImage3 = secondaryImage3,
                    Instock = model.Instock,
                };
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();

        }

        private string UploadedFile(IFormFile imageFile)
        {
            
            string uniqueFileName = null;

            if (imageFile != null)
            {
                string uploadsFolder = Path.Combine(_HostEnvironment.WebRootPath, "Images");
                //uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                uniqueFileName = imageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }



        private void PopulateCategoriesDropDownList(object selectedCategory = null)
        {
            var categoryQuery = from d in _context.tbl_NaturLab_Category
                                   orderby d.CategoryDescription
                                   select d;
            ViewBag.CategoryID = new SelectList(categoryQuery.AsNoTracking(), "CategoryID", "CategoryDescription", selectedCategory);

        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var product =await _context.tbl_NaturLab_Product.Include(c => c.Category).AsNoTracking().FirstOrDefaultAsync(m=>m.ProductID==id); ;

            var product = await _context.tbl_NaturLab_Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            PopulateCategoriesDropDownList(product.CategoryID);
            return View(product);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ProductID,Name,Description,Price,CategoryID,InStock, PrimaryImage, SecondaryImage1, SecondaryImage2,SecondaryImage3")] Product product)
        public async Task<IActionResult> Edit(Product product)
        {
            //if (id != product.ProductID)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.tbl_NaturLab_Product
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {


            var product = await _context.tbl_NaturLab_Product.FindAsync(id);
            if (!string.IsNullOrEmpty(product.PrimaryImage))
            {
                Deletefile(product.PrimaryImage);
            }
            if (!string.IsNullOrEmpty(product.SecondaryImage1))
            {
                Deletefile(product.SecondaryImage1);
            }
            if (!string.IsNullOrEmpty(product.SecondaryImage2))
            {
                Deletefile(product.SecondaryImage2);
            }
            if (!string.IsNullOrEmpty(product.SecondaryImage3))
            {
                Deletefile(product.SecondaryImage3);
            }
  

            _context.tbl_NaturLab_Product.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public void Deletefile(string fname)
        {
            string _imageToBeDeleted = Path.Combine(_HostEnvironment.WebRootPath, "Images\\", fname);
            if ((System.IO.File.Exists(_imageToBeDeleted)))
            {
                System.IO.File.Delete(_imageToBeDeleted);
            }
            //return RedirectToAction("index", new { deleted = fname });
        }


        private bool ProductExists(int id)
        {
            return _context.tbl_NaturLab_Product.Any(e => e.ProductID == id);
        }
    }
}
