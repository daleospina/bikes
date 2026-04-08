using System.ComponentModel.DataAnnotations;

namespace cicloso.bikes.core.Models;

public class Bike
{
    public int Id { get; set; }

    [Required]
    public string Brand { get; set; }

    [Required]
    public string Model { get; set; }

    [Required]
    public string Fork { get; set; }

    [Required]
    public string Groupset { get; set; }

    [Required]
    public string Brakes { get; set; }

    [Required]
    public int Price { get; set; }

    [Required]
    public string Url { get; set; }

    public string Image { get; set; }

    public string Notes { get; set; }
}