using System.ComponentModel.DataAnnotations;
namespace StudentMarksMasterDetail.Models;
public class Mark
{
    public int Id { get; set; }
    [Required, StringLength(100)] public string Exam { get; set; } = string.Empty;
    [Range(0, 100)] public decimal MarksObtained { get; set; }
    public int StudentId { get; set; }
    public Student? Student { get; set; }
}