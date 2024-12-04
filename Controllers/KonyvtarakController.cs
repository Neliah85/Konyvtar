using Konyvtar.DTOs;
using Konyvtar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace konyvtar.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KonyvtarakController : ControllerBase
    {
        [HttpGet]

        public IActionResult Get()
        {
            using (var context = new KonyvtarakContext())
            {
                try
                {
                    return Ok(context.Konyvtaraks.ToList());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
        }

        [HttpGet("GetFull")]

        public IActionResult GetFull()
        {
            using (var context = new KonyvtarakContext())
            {
                try
                {
                    return Ok(context.Konyvtaraks.Include(k => k.IrszNavigation.Megye).ToList());//települések
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
        }

        [HttpGet("GetFullAsync")]

        public async Task<IActionResult> GetFullAsync()
        {
            using (var context = new KonyvtarakContext())
            {
                try
                {
                    return Ok(await context.Konyvtaraks.Include(k => k.IrszNavigation.Megye).ToListAsync());//települések
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
        }


        [HttpGet("GetId")]

        public IActionResult GetId(int id)
        {
            using (var context = new KonyvtarakContext())
            {
                try
                {
                    return Ok(context.Konyvtaraks.Include(k => k.IrszNavigation.Megye).FirstOrDefault(k => k.Id == id));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
        }

        [HttpGet("GetIdAsync")]

        public async Task<IActionResult> GetIdAsync(int id)
        {
            using (var context = new KonyvtarakContext())
            {
                try
                {
                    return Ok(await context.Konyvtaraks.Include(k => k.IrszNavigation.Megye).FirstOrDefaultAsync(k => k.Id == id));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
        }

        [HttpGet("GetIdDTOAsync")]

        public async Task<IActionResult> GetIdDTOAsync(int id)
        {
            using (var context = new KonyvtarakContext())
            {
                try
                {
                    return Ok(await context.Konyvtaraks.Include(k => k.IrszNavigation.Megye).Select(k => new KonyvtarakDTO 
                    { 
                        Id = k.Id, 
                        KonyvtarNev = k.KonyvtarNev, 
                        Irsz = k.Irsz, Cim = k.Cim, 
                        TelepNev = k.IrszNavigation.TelepNev, 
                        MegyeNev = k.IrszNavigation.Megye.MegyeNev 
                    }
                    ).FirstOrDefaultAsync(k => k.Id == id));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
        }

        [HttpGet("GetFullDTOAsync")]

        public async Task<IActionResult> GetFullDTOAsync()
        {
            using (var context = new KonyvtarakContext())
            {
                try
                {
                    return Ok(await context.Konyvtaraks.Include(k => k.IrszNavigation.Megye).Select(k => new KonyvtarakDTO
                    {
                        Id = k.Id,
                        KonyvtarNev = k.KonyvtarNev,
                        Irsz = k.Irsz,
                        Cim = k.Cim,
                        TelepNev = k.IrszNavigation.TelepNev,
                        MegyeNev = k.IrszNavigation.Megye.MegyeNev
                    }).ToListAsync());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
        }

        [HttpPost]

        public IActionResult Post(Konyvtarak konyvtar)
        {
            using (var context = new KonyvtarakContext())
            {
                try
                {
                    context.Konyvtaraks.Add(konyvtar);
                    context.SaveChanges();
                    return StatusCode(200, "Új könyvtár adatai sikeresen eltárolva.");
                }

                catch (Exception ex)
                {
                    return StatusCode(400, ex.Message);
                }
            
            }
        }

        [HttpPut]

        public async Task<IActionResult> Put(Konyvtarak konyvtar)
        {
            using (var context = new KonyvtarakContext())
            {
                try
                {
                    context.Konyvtaraks.Update(konyvtar);
                    await context.SaveChangesAsync();
                    return StatusCode(200, "A könyvtár adatai sikeresen módosítva.");
                }

                catch (Exception ex)
                {
                    return StatusCode(400, ex.Message);
                }

            }
        }

        [HttpDelete("id")]

        public async Task<IActionResult> Delete(int id)
        {
            using (var context = new KonyvtarakContext())
            {
                try
                {
                    Konyvtarak konyvtar = new Konyvtarak {Id = id};
                    context.Konyvtaraks.Update(konyvtar);
                    await context.SaveChangesAsync();
                    return StatusCode(200, "A könyvtár adatai sikeresen törölve.");
                }

                catch (Exception ex)
                {
                    return StatusCode(400, ex.Message);
                }

            }
        }
    }
}