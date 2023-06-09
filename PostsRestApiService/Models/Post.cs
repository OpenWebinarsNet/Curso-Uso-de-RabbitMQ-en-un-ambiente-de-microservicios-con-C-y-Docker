﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostsRestApiService.Models
{
    [Table("Post")]
    public partial class Post
    {
        [Key]
        public Guid PostId { get; set; }
        [StringLength(50)]
        public string PostTitle { get; set; }
        [StringLength(250)]
        public string PostText { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PostCreationDate { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("Posts")]
        public virtual User User { get; set; }
    }
}