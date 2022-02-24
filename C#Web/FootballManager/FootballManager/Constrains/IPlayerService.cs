using FootballManager.ViewModels;

namespace FootballManager.Constrains
{
    public interface IPlayerService
    {
        public (bool isValid, string errors) ValidateAddPlayerForm(AddPlayerFormModel model);

        public bool AddPlayer(AddPlayerFormModel model);

        public ICollection<ShowPlayerModel> GetAllPlayers();

        public (bool isDone, string errors) AddPlayerToCollection(int playerId, string userId);

        public ICollection<ShowPlayerModel> GetPlayerCollection(string userId);

        public void RemovePlayer(int playerId, string userId);

    }
}
