using System;
using System.Collections.Generic;
using System.Text;

namespace NewsFeeds.BLL.Enums
{
    public enum FeedResponse
    {
        Ok,
        InvalidTitle,
        FeedWithTitleAlreadyExists,
        FeedNotExist,
        FeedCollectionNotExist
    }
}
