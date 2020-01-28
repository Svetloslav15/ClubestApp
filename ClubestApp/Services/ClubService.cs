namespace ClubestApp.Services
{
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using ClubestApp.Data.Models.Enums;
    using ClubestApp.Models.InputModels;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class ClubService
    {
        private ApplicationDbContext dbContext;

        private readonly string interestsPath = $"{Directory.GetCurrentDirectory()}/Common/Json/Interests.json";

        public ClubService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Club AddClub(AddClubInputModel model)
        {
            Club newClub = new Club
            {
                Name = model.Name,
                Fee = (decimal)model.Fee,
                PriceType = (PriceType)Enum.Parse(typeof(PriceType), model.PriceType, true),
                IsPublic = (bool)model.IsPublic
            };

            var result = this.dbContext.Clubs.Add(newClub);
            this.dbContext.SaveChanges();

            return result.Entity;
        }

        public Dictionary<string, Dictionary<string, string>> GetInterests()
        {
            string interests = "";
            using (StreamReader reader = new StreamReader(interestsPath, Encoding.GetEncoding("windows-1251")))
            {
                interests = reader.ReadToEnd();
            }

            var interestsJson = JsonConvert.DeserializeObject<Dictionary<string,
                                Dictionary<string, string>>>(interests);
            return interestsJson;
        }
    }
}
