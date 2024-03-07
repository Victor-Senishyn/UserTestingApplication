﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserTestingApplication.Models
{
    [Table(nameof(ApplicationUserTest))]
    public class ApplicationUserTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsCompleted { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(TestId))]
        public int TestId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Test> Tests { get; set; }
    }
}