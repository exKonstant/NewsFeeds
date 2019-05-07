using System;
using System.Collections.Generic;
using System.Text;

namespace NewsFeeds.BLL.Enums
{
    public enum FeedCollectionResponse
    {
        Ok, 
        InvalidName,
        FeedCollectionWithThisNameAlreadyExists,
        FeedCollectionNotExist,
        UserNotExist
    }
}
