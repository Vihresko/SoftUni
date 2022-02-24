using FootballManager.Constrains;
using FootballManager.Data;
using FootballManager.Data.Models;
using FootballManager.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace FootballManager.Services
{
    using static Constrains.Constant;
    public class PlayerService : IPlayerService
    {
        private readonly FootballManagerDbContext data;
        public PlayerService(FootballManagerDbContext _data)
        {
            data = _data;
        }
        public bool AddPlayer(AddPlayerFormModel model)
        {
            bool isDone = false;
            var newPlayer = new Player()
            {
                FullName = model.FullName,
                ImageUrl = model.ImageUrl,
                Position = model.Position,
                Speed = model.Speed,
                Endurance = model.Endurance,
                Description = model.Description,
                Id = model.Id,
            };

            try
            {
                data.Players.Add(newPlayer);
                data.SaveChanges();
                isDone = true;
            }
            catch { }
            return isDone;
        }

        public (bool isDone, string errors) AddPlayerToCollection(int playerId, string userId)
        {
            bool isDone = true;
            var errors = new StringBuilder();
            var checkPlayer = data.UserPlayers.FirstOrDefault(up => up.PlayerId == playerId && up.UserId == userId);
            
            if(checkPlayer != null)
            {
                isDone = false;
                errors.AppendLine("This player is already in your collection!");
                return (isDone, errors.ToString());
            }

            var player = data.Players.Where(p => p.Id == playerId).FirstOrDefault();
            var user = data.Users.Where(u => u.Id == userId).FirstOrDefault();
            UserPlayer map = new UserPlayer()
            {
                PlayerId = playerId,
                UserId = userId,
                Player = player,
                User = user
            };
            user.UserPlayers.Add(map);

           data.SaveChanges();
           return (isDone, "");
        }

        public ICollection<ShowPlayerModel> GetAllPlayers()
        {
            var allPlayers = data.Players.ToArray();
            List<ShowPlayerModel> showPlayers = new List<ShowPlayerModel>();
            foreach (var p in allPlayers)
            {
                var player = new ShowPlayerModel()
                {
                    Description = p.Description,
                    Endurance = p.Endurance,
                    FullName = p.FullName,
                    ImageUrl = p.ImageUrl,
                    Position = p.Position,
                    Speed = p.Speed,
                    Id = p.Id
                };
                showPlayers.Add(player);
            }
            return showPlayers;
        }

        public ICollection<ShowPlayerModel> GetPlayerCollection(string userId)
        {
            var user = data.Users.Where(u => u.Id == userId).Include(p => p.UserPlayers).ThenInclude(p => p.Player).FirstOrDefault();

           
            List<ShowPlayerModel> showPlayers = new List<ShowPlayerModel>();
            foreach (var p in user.UserPlayers)
            {
                var player = new ShowPlayerModel()
                {
                    Description = p.Player.Description,
                    Endurance = p.Player.Endurance,
                    FullName = p.Player.FullName,
                    ImageUrl = p.Player.ImageUrl,
                    Position = p.Player.Position,
                    Speed = p.Player.Speed,
                    Id = p.PlayerId
                };
                showPlayers.Add(player);
            }
            return showPlayers;
        }

        public void RemovePlayer(int playerId, string userId)
        {
            var userPlayer = data.UserPlayers.Where(up => up.UserId == userId && up.PlayerId == playerId).FirstOrDefault();
            data.UserPlayers.Remove(userPlayer);
            data.SaveChanges();
            
        }

        public (bool isValid, string errors) ValidateAddPlayerForm(AddPlayerFormModel model)
        {
            bool isValid = true;
            var errors = new StringBuilder();
            if(model.FullName == null || model.FullName.Length < FULLNAME_MIN_LENGTH || model.FullName.Length > FULLNAME_MAX_LENGTH)
            {
                isValid = false;
                errors.AppendLine($"Full Name must be between {FULLNAME_MIN_LENGTH} and {FULLNAME_MAX_LENGTH} characters!");
            }

            if(model.ImageUrl == null)
            {
                isValid = false;
                errors.AppendLine("Image URL must be valid");
            }

            if(model.Position == null)
            {
                isValid = false;
                errors.AppendLine("Position must be valid");
            }

            if(model.Speed == null|| model.Speed < SPEED_MIN_VALUE || model.Speed > SPEED_MAX_VALUE)
            {
                isValid = false;
                errors.AppendLine($"Speed must be between '{SPEED_MIN_VALUE}' and '{SPEED_MAX_VALUE}'!");
            }
            if(model.Endurance == null|| model.Endurance < ENDURANCE_MIN_VALUE || model.Endurance > ENDURANCE_MAX_VALUE)
            {
                isValid = false;
                errors.AppendLine($"Endurance must be between '{ENDURANCE_MIN_VALUE}' and '{ENDURANCE_MAX_VALUE}'!");
            }

            return (isValid, errors.ToString());
        }
    }
}
