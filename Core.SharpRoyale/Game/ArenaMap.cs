using Core.SharpRoyale.Entities;

namespace Core.SharpRoyale;

public class ArenaMap
{
    private const int _width = 18;
    private const int _height = 32;

    private readonly Tile[,] _tiles;
    public List<Entity> Entities;

    public ArenaMap()
    {
        _tiles = new Tile[_width, _height];

        // default
        for (var x = 0; x < _width; x++)
        for (var y = 0; y < _height; y++)
            _tiles[x, y].Kind = TileKind.Standard;

        // River
        for (var x = 0; x < _width; x++)
            _tiles[x, 15].Kind = TileKind.River;

        // Bridges
        _tiles[5, 15].Kind = TileKind.Bridge;
        _tiles[12, 15].Kind = TileKind.Bridge;
    }

    public ArenaMap AddPlayerTowers((Player p1, Player p2) players, Match match)
    {
        // Player 1 towers (bottomjj)
        Entities.Add(new Tower(players.p1.Id, match).ProcessDeployment(9, 28));  // King Tower
        Entities.Add(new Tower(players.p1.Id, match).ProcessDeployment(4, 28));  // Left Princess
        Entities.Add(new Tower(players.p1.Id, match).ProcessDeployment(14, 28)); // Right Princess

        // Player 2 towers (top)
        Entities.Add(new Tower(players.p2.Id, match).ProcessDeployment(9, 4));   // King Tower
        Entities.Add(new Tower(players.p2.Id, match).ProcessDeployment(4, 4));   // Left Princess
        Entities.Add(new Tower(players.p2.Id, match).ProcessDeployment(14, 4));  // Right Princess

        return this;
    }

    public bool CheckIfEntityCanBeDeployed(Entity entity)
    {
        //TODO: Another Days Work
        return true;
    }
}
