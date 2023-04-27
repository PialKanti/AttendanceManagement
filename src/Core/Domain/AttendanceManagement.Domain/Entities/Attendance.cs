﻿using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.Domain.Entities
{
    public class Attendance : BaseEntity
    {
        [Required]
        public IAttendee Attendee { get; set; }
        [Required]
        public long EntryTimestamp { get; set; }
        public long? ExitTimestamp { get; set; }
        [Required]
        public DateTime EntryDate { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public int Year { get; set; }
    }
}
