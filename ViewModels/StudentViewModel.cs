using System.ComponentModel.DataAnnotations;
namespace StudentMarksMasterDetail.ViewModels;
public class StudentViewModel
{
    public int Id { get; set; }
    [Required, StringLength(100), Display(Name = "Student name")] public string Name { get; set; } = string.Empty;
    [Required] public string Gender { get; set; } = string.Empty;
    public List<MarkViewModel> Marks { get; set; } = new();
}
public class MarkViewModel
{
    public int Id { get; set; }
    [Required, StringLength(100)] public string Exam { get; set; } = string.Empty;
    [Range(0, 100), Display(Name = "Marks obtained")] public decimal MarksObtained { get; set; }
}