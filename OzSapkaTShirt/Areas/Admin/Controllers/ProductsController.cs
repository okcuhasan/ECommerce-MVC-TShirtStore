using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OzSapkaTShirt.Data;
using OzSapkaTShirt.Models;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Authorization;

namespace OzSapkaTShirt.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Administrator")]
    public class ProductsController : Controller
    {
        private readonly ApplicationContext _context;

        public ProductsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
              return _context.Products != null ? 
                          View(await _context.Products.ToListAsync()) :
                          Problem("Entity set 'ApplicationContext.Products'  is null.");
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Size,Model,Price,Fabric,Color,Image")] Product product)
        {
            MemoryStream target, reSizedTarget;
            Image reSizedImage, originalImage;
            EncoderParameter qualityParameter;
            EncoderParameters encoderParameters;
            ImageCodecInfo[] allCoDecs;
            ImageCodecInfo jPEGCodec = null;

            if (ModelState.IsValid)
            {
                if (product.Image != null)
                {
                    encoderParameters = new EncoderParameters(1);
                    qualityParameter = new EncoderParameter(Encoder.Quality, 60L);
                    encoderParameters.Param[0] = qualityParameter;
                    reSizedTarget = new MemoryStream();
                    allCoDecs = ImageCodecInfo.GetImageEncoders();
                    foreach (ImageCodecInfo coDec in allCoDecs)
                    {
                        if (coDec.FormatDescription == "JPEG")
                        {
                            jPEGCodec = coDec;
                        }
                    }
                    target = new MemoryStream();
                    product.Image.CopyTo(target); //Dosyayı stream'a kopyala
                    originalImage = Image.FromStream(target);
                    reSizedImage = ReSize(originalImage, 300, 400);
                    reSizedImage.Save(reSizedTarget, jPEGCodec, encoderParameters);
                    product.DBImage = reSizedTarget.ToArray();
                    reSizedImage = ReSize(originalImage, 150, 200);
                    reSizedImage.Save(reSizedTarget, jPEGCodec, encoderParameters);
                    product.ThumbNail = reSizedTarget.ToArray();
                }
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Description,Size,Model,Price,Fabric,Color,DBImage,ThumbNail")] Product product)
        {
            //Create'teki bütün resim işleri Edit'te de olmalı
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //bu ürünün bulunduğu sepetleri güncelle
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(long id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private Image ReSize(Image originalImage, int newWidth, int newHeight)
        {
            Graphics graphicsHandle;
            double targetRatio = (double)newWidth / (double)newHeight;
            double newRatio = (double)originalImage.Width / (double)originalImage.Height;
            int targetWidth = newWidth;
            int targetHeight = newHeight;
            int newOriginX = 0;
            int newOriginY = 0;
            Image newImage = new Bitmap(newWidth, newHeight);

            if (newRatio > targetRatio)
            {
                targetWidth = (int)(originalImage.Width / ((double)originalImage.Height / newHeight));
                newOriginX = (newWidth - targetWidth) / 2;
            }
            else
            {
                if (newRatio < targetRatio)
                {
                    targetHeight = (int)(originalImage.Height / ((double)originalImage.Width / newWidth));
                    newOriginY = (newHeight - targetHeight) / 2;
                }
            }
            graphicsHandle = Graphics.FromImage(newImage);
            graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphicsHandle.CompositingQuality = CompositingQuality.HighQuality;
            graphicsHandle.SmoothingMode = SmoothingMode.HighQuality;
            graphicsHandle.DrawImage(originalImage, newOriginX, newOriginY, targetWidth, targetHeight);
            return newImage;
        }
    }
}
