//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComputerGames.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class CompGame
    {
        public int Id { get; set; }

        [Display(Name = "Game Title")]
        [Required]
        public string GameTitle { get; set; }

        [Required]
        public string Platform { get; set; }

        [Required]
        public string Genre { get; set; }


        [Display(Name = "Age Rating")]
        [Required]
        public string AgeRating { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ReleaseDate { get; set; }

        [DataType(DataType.Currency)]
        public Nullable<decimal> Price { get; set; }

        [Required]
        public string Picture { get; set; }

        [Required]
        public string Preview { get; set; }

        [Required]
        public Nullable<int> Likes { get; set; }

        [Required]
        public Nullable<int> Dislikes { get; set; }
    }
}
