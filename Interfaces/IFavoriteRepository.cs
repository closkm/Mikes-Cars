﻿namespace MikesCars.Interfaces
{
    public interface IFavoriteRepository
    {
        void AddToFavTable(int userId, int listingId);
    }
}