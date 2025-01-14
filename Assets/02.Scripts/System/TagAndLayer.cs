using System;

public static class TagAndLayer
{
    public enum Tag { Untagged, Player, Ground, Area, Enemy, PlayerBullet, Exp, Magnet, EnemyBullet }

    public enum Layer { Default, Area, PlayerBullet, Player, Enemy, Exp, Magnet, EnemyBullet, Spawner }

    private static readonly string[] tags = Enum.GetNames(typeof(Tag));
    private static readonly string[] layers = Enum.GetNames(typeof(Layer));


    public static string GetTag(Tag tag)
    {
        return tags[(int)tag];
    }
    public static string GetLayer(Layer layer)
    {
        return layers[(int)layer];
    }
}
