﻿using System.ComponentModel.DataAnnotations.Schema;

namespace UserTestingApplication.Repositories.Filters
{
    public class CompletedTestFilter
    {
        public int? Id { get; set; }
        public string? ApplicationUserId { get; set; }
        public int? TestId { get; set; }
        public int? Score { get; set; }
    }
}
