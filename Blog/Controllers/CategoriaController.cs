using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blog.Models.Blog.Categoria;

namespace Blog.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly Database _context;

        public CategoriaController()
        {
            _context = new Database();
        }

        // GET: Categoria
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        // GET: Categoria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaEntity = await _context.Categorias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaEntity == null)
            {
                return NotFound();
            }

            return View(categoriaEntity);
        }

        // GET: Categoria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] CategoriaEntity categoriaEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriaEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaEntity);
        }

        // GET: Categoria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaEntity = await _context.Categorias.FindAsync(id);
            if (categoriaEntity == null)
            {
                return NotFound();
            }
            return View(categoriaEntity);
        }

        // POST: Categoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] CategoriaEntity categoriaEntity)
        {
            if (id != categoriaEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaEntityExists(categoriaEntity.Id))
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
            return View(categoriaEntity);
        }

        // GET: Categoria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaEntity = await _context.Categorias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaEntity == null)
            {
                return NotFound();
            }

            return View(categoriaEntity);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriaEntity = await _context.Categorias.FindAsync(id);
            _context.Categorias.Remove(categoriaEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaEntityExists(int id)
        {
            return _context.Categorias.Any(e => e.Id == id);
        }
    }
}
