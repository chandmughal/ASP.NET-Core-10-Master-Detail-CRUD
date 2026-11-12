using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentMarksMasterDetail.Data;
using StudentMarksMasterDetail.Models;
using StudentMarksMasterDetail.ViewModels;
namespace StudentMarksMasterDetail.Controllers;
public class StudentsController(AppDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var students = await db.Students.Include(s => s.Marks).AsNoTracking().OrderBy(s => s.Name).ToListAsync();
        return View(students);
    }
    [HttpGet] public IActionResult Create() => View(new StudentViewModel { Marks = new() { new() } });
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StudentViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);
        var student = new Student { Name = vm.Name, Gender = vm.Gender,
            Marks = vm.Marks.Select(m => new Mark { Exam = m.Exam, MarksObtained = m.MarksObtained }).ToList() };
        db.Students.Add(student);
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var student = await db.Students.Include(s => s.Marks).AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        if (student is null) return NotFound();
        return View(new StudentViewModel { Id = student.Id, Name = student.Name, Gender = student.Gender,
            Marks = student.Marks.Select(m => new MarkViewModel { Id = m.Id, Exam = m.Exam, MarksObtained = m.MarksObtained }).ToList() });
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, StudentViewModel vm)
    {
        if (id != vm.Id) return BadRequest();
        var student = await db.Students.Include(s => s.Marks).FirstOrDefaultAsync(s => s.Id == id);
        if (student is null) return NotFound();
        if (!ModelState.IsValid) return View(vm);
        student.Name = vm.Name; student.Gender = vm.Gender;
        var submittedIds = vm.Marks.Where(m => m.Id > 0).Select(m => m.Id).ToHashSet();
        foreach (var oldRow in student.Marks.Where(m => !submittedIds.Contains(m.Id)).ToList()) db.Marks.Remove(oldRow);
        foreach (var item in vm.Marks)
        {
            if (item.Id == 0) student.Marks.Add(new Mark { Exam = item.Exam, MarksObtained = item.MarksObtained });
            else
            {
                var oldRow = student.Marks.FirstOrDefault(m => m.Id == item.Id);
                if (oldRow is null) return BadRequest("Invalid mark row.");
                oldRow.Exam = item.Exam; oldRow.MarksObtained = item.MarksObtained;
            }
        }
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await db.Students.FindAsync(id);
        if (student is null) return NotFound();
        db.Students.Remove(student);
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}