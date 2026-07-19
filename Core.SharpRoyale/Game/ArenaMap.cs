using Core.SharpRoyale.Entities;
using Core.SharpRoyale.GameServices.ActionListService;

namespace Core.SharpRoyale;

public class ArenaMap
{
    private const int _width = 18;
    private const int _height = 32;

    private readonly Tile[,] _tiles;

    public ArenaMap()
    {
        _tiles = new Tile[_width, _height];

        // default
        for (var x = 0; x < _width; x++)
        for (var y = 0; y < _height; y++)
        {
            _tiles[x, y] = new Tile();
            _tiles[x, y].Kind = TileKind.Standard;
        }

        // River
        for (var x = 0; x < _width; x++)
            _tiles[x, 15].Kind = TileKind.River;

        // Bridges
        _tiles[5, 15].Kind = TileKind.Bridge;
        _tiles[12, 15].Kind = TileKind.Bridge;
    }

    public List<Entity> Entities { get; } = new();

    public ArenaMap AddPlayerTowers((Player p1, Player p2) players, Match match)
    {
        // Player 1 towers (bottom)
        ActionListService.AppendActionListSpawn(
            new ActionListValueSpawn(new Position(9, 30), EntityId.Tower), // King, pulled back near bottom edge
            new King(players.p1.Id, match),
            match
        );
        ActionListService.AppendActionListSpawn(
            new ActionListValueSpawn(new Position(3, 28), EntityId.Tower),
            new Tower(players.p1.Id, match),
            match
        ); // Left Princess
        ActionListService.AppendActionListSpawn(
            new ActionListValueSpawn(new Position(14, 28), EntityId.Tower),
            new Tower(players.p1.Id, match),
            match
        ); // Right Princess

        // Player 2 towers (top)
        ActionListService.AppendActionListSpawn(
            new ActionListValueSpawn(new Position(9, 2), EntityId.Tower), // King, pulled back near top edge
            new King(players.p2.Id, match),
            match
        ); // King Tower
        ActionListService.AppendActionListSpawn(
            new ActionListValueSpawn(new Position(3, 4), EntityId.Tower),
            new Tower(players.p2.Id, match),
            match
        ); // Left Princess
        ActionListService.AppendActionListSpawn(
            new ActionListValueSpawn(new Position(14, 4), EntityId.Tower),
            new Tower(players.p2.Id, match),
            match
        ); // Right Princess

        return this;
    }

    public bool CheckIfEntityCanBeDeployed(Entity entity)
    {
        //TODO: Another Days Work
        return true;
    }
}
