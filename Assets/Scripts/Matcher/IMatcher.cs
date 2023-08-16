using System.Collections.Generic;

public interface IMatcher
{
    MatchInfo Match(IEnumerable<ICard> cards);
}