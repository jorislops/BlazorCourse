using System.ComponentModel.DataAnnotations;

namespace BlazorCourse.Components.Pages.Databases.BierExample.Model;

public class Bier
{
    public int Biercode { get; set; }

    [Required, MinLength(2, ErrorMessage = "Naam moet minimaal twee charachters bevatten"), MaxLength(50)]
    public string Naam { get; set; } = null!;

    [Required, MinLength(2), MaxLength(30)]
    public string Type { get; set; } = null!;

    [Required, MaxLength(20)] 
    public string Stijl { get; set; } = null!;
    
    [Required, Range(0, 16, ErrorMessage = "Alcoholpercentage moet tussen 0 en 16 liggen")]
    public double? Alcohol { get; set; }
    
    [Required(ErrorMessage = "Je moet een brouwer invullen")]
    public int? Brouwcode { get; set; }
    
    public Brouwer Brouwer { get; set; } = null!;
}