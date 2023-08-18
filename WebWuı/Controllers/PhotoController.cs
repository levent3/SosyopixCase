using DataLayer.Absract;
using DataLayer.Concreate;
using Entities.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebWuı.Models;

namespace WebWuı.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoDAL _photoDAL;

        public PhotoController(IPhotoDAL photoDAL)
        {
            _photoDAL = photoDAL;
        }



        public async Task<IActionResult> Index()
        {


            var result=await _photoDAL.GetAll();

            if (result == null)
            {


                return View(result);
            }


            return View(result);
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {


            var createDto = new PhotoDto();


            return View(createDto);
        }



        [HttpPost]
        public async Task<IActionResult> Create(PhotoDto photoDto)
        {

                if (!ModelState.IsValid)
                {
                    return View(photoDto);
                }

              
              
              
                    Photo photo = new Photo
                    {
                        Id = photoDto.Id,
                        Tag = photoDto.Tag,
                      Title = photoDto.Title,   
                    };


                    if (photoDto.ImageFile != null)
                    {
                        var extent = Path.GetExtension(photoDto.ImageFile.FileName);
                        var randomName = ($"{Guid.NewGuid()}{extent}");
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Uploads", randomName);
                        photo.Image = "Uploads\\" + randomName;
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await photoDto.ImageFile.CopyToAsync(stream);
                        }

                        using (MemoryStream ms = new MemoryStream())
                        {
                            //postedFile.CopyTo(ms);	
                            photoDto.ImageFile.CopyTo(ms);
                        }
                    }

                    var sonuc = await _photoDAL.CreateAsync(photo);

                    if (sonuc > 0)
                    {
                        return RedirectToAction("Index", "Photo");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Bilinmeyen bir hata olustu. Daha Sonra Tekrar Deneyiniz");

                    }
                

            return View(photoDto); 
         

        }



        [HttpGet]

        public async Task<IActionResult> Update(int Id)
        {
            var photo = _photoDAL.FindAsync(p => p.Id == Id).Result;
            PhotoDto updateDTO = new PhotoDto()


            {
                Id = photo.Id,
                Title = photo.Title,
                Tag = photo.Tag,
           
        
            };
            return View(updateDTO);
        }


        [HttpPost]
        public async Task<IActionResult> Update(PhotoDto photoDto)
        {
            if (!ModelState.IsValid)
            {
                return View(photoDto);

            }

            var photo2 = _photoDAL.FindAsync(p => p.Id == photoDto.Id).Result;



            photo2.Title = photoDto.Title;
            photo2.Tag = photoDto.Tag;
          

            var sonuc = await _photoDAL.UpdateAsync(photo2);
            if (sonuc > 0)
            {
                return RedirectToAction("Index", "Photo");
            }
            else
            {
                ModelState.AddModelError("", "Bilinmeyen bir hata olustu. Lutfen Biraz sonra tekrar denbeyiniz");

                return View(photoDto);
            }

        }



        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var photo = _photoDAL.FindAsync(p => p.Id == Id).Result;
            PhotoDto deleteDTO = new PhotoDto()


            {
                Id = photo.Id,
                Title = photo.Title,
                Tag = photo.Tag,


            };
            return View(deleteDTO);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(PhotoDto photoDto)
        {
            

            var photo2 = _photoDAL.FindAsync(p => p.Id == photoDto.Id).Result;





            var sonuc = await _photoDAL.DeleteAsync(photo2);
            if (sonuc > 0)
            {
                return RedirectToAction("Index", "Photo");
            }
            else
            {
                ModelState.AddModelError("", "Bilinmeyen bir hata olustu. Lutfen Biraz sonra tekrar denbeyiniz");

                return View(photoDto);
            }

        }


    }
}
