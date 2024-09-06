using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models;

public class Suivi
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SuiviId { get; set; }
    public string? Title { get; set; } = "Missing Title";
    public int? Price { get; set; }
    public string? Note { get; set; }
    public DateTime? Deadline { get; set; }
    public string? LinkOffer { get; set; } // TODO : convert into Uri
    public DateTime? LastInteractionDate { get; set; } // TODO : convert into DateOnly
    public Interaction NextInteraction { get; set; } = Interaction.ToEmail;
    [Required]
    public Status Status { get; set; }
    [Required]
    public Priority Priority { get; set; }
    [Required]
    public Type Type { get; set; }

    public List<Contact> Contacts { get; set; } // DTO

    [ForeignKey(nameof(AddressId))]
    public int? AddressId { get; set; } // DTO
    
    [ForeignKey(nameof(UserId))]
    public Guid UserId { get; set; } // Owner of the Suivi


    public DateTime CreationDate { get; set; } = DateTime.Now; // TODO : convert into DateOnly
    public DateTime? UpdateDate { get; set; } // TODO : convert into DateOnly

    //public DateOnly CreationDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    //public DateOnly? UpdateDate { get; set; }
}

// To store them as strings, use a value converter or the HasConversion method in the OnModelCreating method.
public enum Type { Job, RealEstate }
public enum Status { NotStarted, InProgress, Done }
public enum Priority { Low, Medium, High }
public enum Interaction { ToWait, ToEmail, ToCall, ToGo }