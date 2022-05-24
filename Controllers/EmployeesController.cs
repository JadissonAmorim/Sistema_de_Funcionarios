using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Empregados.Models;
using Web_Empregados.Models;

namespace SistemaDeEmpregados.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string id)
        {
            var Employees = from m in _context.Employees
                            select m;

            if (!string.IsNullOrEmpty(id))
            {
                Employees = Employees.Where(s => s.FullName.Contains(id) || s.Sector.Contains(id));
               
            }
            ViewBag.TotalSalary = Employees.Sum(X => X.Salary);
            ViewBag.TotalAliment = Employees.Sum(Y => Y.FoodVoucher);
            ViewBag.TotalTransport = Employees.Sum(Y => Y.TransportationVoucher);
            ViewBag.Total = "Gastos totais: R$ ";
            ViewBag.Total += ViewBag.TotalSalary + ViewBag.TotalAliment + ViewBag.TotalTransport + ",00";
            ViewBag.TotalSalary = "Gastos com Salário: R$ ";
            ViewBag.TotalSalary += Employees.Sum(X => X.Salary) + ",00";
            ViewBag.TotalAliment = "Gastos com Vale Alimentação: R$ ";
            ViewBag.TotalAliment += Employees.Sum(Y => Y.FoodVoucher) + ",00";
            ViewBag.TotalTransport = "Gastos com Vale Transporte: R$ ";
            ViewBag.TotalTransport += Employees.Sum(Y => Y.TransportationVoucher) + ",00";
            if (ViewBag.Total == "Gastos totais: R$ 0,00")
            {
                ViewBag.Total = "";
                ViewBag.TotalSalary = "";
                ViewBag.TotalAliment = "";
                ViewBag.TotalTransport = "";
            }

            return View(await Employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,FullName,Sector,Salary,FoodVoucher,TransportationVoucher")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,FullName,Sector,Salary,FoodVoucher,TransportationVoucher")] Employee employee)
        {
            if (id != employee.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.id))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'EmployeeContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
          return (_context.Employees?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
