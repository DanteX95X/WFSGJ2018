using System.ComponentModel;

namespace WFS
{
    public enum Action
    {
        Death = -3,
        NegativeSecond = -2,
        NegativeFirst = -1,
        Timeout = 0,
        PositiveFirst = 1,
        PositiveSecond = 2
    }
}