using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using FootballManager.Constrains;
using FootballManager.ViewModels;

namespace FootballManager.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayerService playerService;
        public PlayersController(Request request, IPlayerService _playerService) : base(request)
        {
            playerService = _playerService;
        }

        public Response Add()
        {
            if (!User.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View(new { IsAuthenticated = true });
        }
        [HttpPost]
        public Response Add(AddPlayerFormModel model)
        {
            (bool isValid, string errors) = playerService.ValidateAddPlayerForm(model);
            if (!isValid)
            {
                return View(new { ErrorMessage = errors, IsAuthenticated = true }, "/Error");
            }

            bool addStatus = playerService.AddPlayer(model);
            if (!addStatus)
            {
                return View(new { ErrorMessage = "Fail to add player in Database!", IsAuthenticated = true }, "/Error");
            }

            return Redirect("/Players/All");
        }

        public Response All()
        {
            var model = playerService.GetAllPlayers();
            return View(new { model, IsAuthenticated = true });
        }

        public Response AddToCollection(string playerId)
        {
            var playerIdToInt = int.Parse(playerId);
            (bool isDone, string errors) = playerService.AddPlayerToCollection(playerIdToInt, User.Id);
            if (!isDone)
            {
                return View(new { ErrorMessage = errors, IsAuthenticated = true }, "/Error");
            }

            return Redirect("/Players/All");
        }

        public Response Collection()
        {
            var model = playerService.GetPlayerCollection(User.Id);
            return View(new { model, IsAuthenticated = true });
        }

        public Response RemoveFromCollection(string playerId)
        {
            var playerIdToInt = int.Parse(playerId);
            playerService.RemovePlayer(playerIdToInt, User.Id);
            return Redirect("/Players/Collection");
        }
    }
}
