using GAMEPORTALCMS.Models.DTO;

namespace GAMEPORTALCMS.Models.Request
{
    public class SavePatchGamesRequest
    {
        public List<SimpleDTO> PatchgamesList { get; set; }
        public int PatchId { get; set; }
    }
}
