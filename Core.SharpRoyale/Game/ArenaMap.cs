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
        ActionListService.AppendActionListSpawnSpecial(
            new ActionListValueSpawn(new Position(2, 2), (int)EntityId.Larry, players.p1),
            match
        );

        ActionListService.AppendActionListSpawnSpecial(
            new ActionListValueSpawn(new Position(9, 30), (int)EntityId.King, players.p1),
            match
        );
        ActionListService.AppendActionListSpawnSpecial(
            new ActionListValueSpawn(new Position(3, 28), (int)EntityId.Tower, players.p1),
            match
        );
        ActionListService.AppendActionListSpawnSpecial(
            new ActionListValueSpawn(new Position(14, 28), (int)EntityId.Tower, players.p1),
            match
        );

        ActionListService.AppendActionListSpawnSpecial(
            new ActionListValueSpawn(new Position(9, 2), (int)EntityId.King, players.p2),
            match
        );
        ActionListService.AppendActionListSpawnSpecial(
            new ActionListValueSpawn(new Position(3, 4), (int)EntityId.Tower, players.p2),
            match
        );
        ActionListService.AppendActionListSpawnSpecial(
            new ActionListValueSpawn(new Position(14, 4), (int)EntityId.Tower, players.p2),
            match
        );

        return this;
    }

    public bool CheckIfEntityCanBeDeployed(int entityId, Position pos, Match match)
    {
        //TODO: Another Days Work
        return true;
    }
}
