using System.ComponentModel.DataAnnotations;
namespace StudentMarksMasterDetail.Models;
public class Student
{
    public int Id { get; set; }
    [Required, StringLength(100)] public string Name { get; set; } = string.Empty;
    [Required, StringLength(10)] public string Gender { get; set; } = string.Empty;
    public ICollection<Mark> Marks { get; set; } = new List<Mark>();
}