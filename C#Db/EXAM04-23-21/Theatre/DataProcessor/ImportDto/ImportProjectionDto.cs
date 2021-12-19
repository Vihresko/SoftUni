using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Theatre.DataProcessor.ImportDto
{
    public class ImportProjectionDto
    {
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        [StringLength(30, MinimumLength = 4)]
        public string Name { get; set; }

        [Required]
        [Range((sbyte)1,10)]
        public sbyte NumberOfHalls { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        [StringLength(30, MinimumLength = 4)]
        public string Director { get; set; }

        [MinLength(1)]
        public ImportTicketDto[] Tickets { get; set; }
    }
}
