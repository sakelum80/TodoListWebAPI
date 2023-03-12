using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Dtos
{
    public class TodoItemCreateDto
    {     
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        public bool IsCompleted { get; set; }
    }
}
