﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;

namespace UniversityCompetition.Models
{
    public class University : IUniversity
    {
        private string[] allowedCategories = new string[] { "Technical", "Economical", "Humanity" };
        
        private int id;
        private string name;
        private string category;
        private int capacity;
        private List<int> requiredSubjects;

        public University(int universityId, string universityName, string category, int capacity, ICollection<int> requiredSubjects)
        {
            Id = universityId;
            Name = universityName;
            Category = category;
            Capacity = capacity;
            this.requiredSubjects = requiredSubjects.ToList();
        }
        
        public int Id
        {
            get { return id; }
            private set
            {
                id = value;
            }
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or whitespace!");
                }
                
                name = value;
            }
        }


        public string Category
        {
            get { return category; }
            private set
            {
                if(!allowedCategories.Contains(value))
                {
                    throw new ArgumentException($"University category {value} is not allowed in the application!");
                }

                category = value;
            }
        }

        public int Capacity
        {
            get { return capacity; }
            private set
            {
                if(value < 0)
                {
                    throw new ArgumentException("University capacity cannot be a negative value!");
                }
                
                capacity = value;
            }
        }

        public IReadOnlyCollection<int> RequiredSubjects => requiredSubjects.AsReadOnly();
    }
}
