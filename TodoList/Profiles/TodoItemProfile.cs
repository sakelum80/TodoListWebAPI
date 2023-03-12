using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Data.Entities;
using TodoList.Dtos;

namespace TodoList.Profiles
{
    public class TodoItemProfile : Profile
    {

        public TodoItemProfile()
        {
            //Source ---> Target
            CreateMap<TodoItem, TodoItemReadDto>();
            CreateMap<TodoItemCreateDto, TodoItem>();
            CreateMap<TodoItemUpdateDto, TodoItem>();
        }
    }    
}
