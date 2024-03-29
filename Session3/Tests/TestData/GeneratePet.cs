﻿using APITraining.Session3.DataModels;

namespace APITraining.Session3.Tests.TestData
{
    public class GeneratePet
    {
        public static PetModel pet()
        {
            CategoryModel categoryData = new CategoryModel()
            {
                Id = 0,
                Name = "string"
            };

            var RandomInt64 = new Random();

            return new PetModel
            {
                Id = RandomInt64.NextInt64(),
                Category = categoryData,
                Name = "Blue",
                PhotoUrls = new string[] { "string" },
                Tags = new CategoryModel[] { categoryData },
                Status = "available"
            };
        }
    }
}
