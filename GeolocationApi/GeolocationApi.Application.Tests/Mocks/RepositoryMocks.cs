﻿using ColocationApi.Domain.Entities;
using GeolocationApi.Application.Contracts.Persistence;
using Moq;

namespace GeolocationApi.Application.Tests.Mock
{
    public static class RepositoryMocks
    {
        public static Mock<IGeolocationRepository> GetGeolocationRepository()
        {
            var locations = GetLocations();
            var mock = new Mock<IGeolocationRepository>();

            mock.Setup(reopo => reopo.GetAllAsync()).ReturnsAsync(locations);

            mock.Setup(repo => repo.GetByIpAsync(It.IsAny<string>())).
                ReturnsAsync((string ip) => locations.FirstOrDefault(g => g.Ip == ip));

            mock.Setup(repo => repo.AddAsync(It.IsAny<Geolocation>()))
                .ReturnsAsync((Geolocation location) =>
                {
                    locations.Add(location);
                    return location;
                });

            mock.Setup(repo => repo.DeleteAsync(It.IsAny<Geolocation>()))
                .ReturnsAsync((Geolocation location) =>
                {
                    locations.Remove(location);
                    return location;
                });

            return mock;
        }

        private static List<Geolocation> GetLocations()
        {
            var location1 = new Geolocation()
            {
                Ip = "8.8.8.8",
                Type = "ipv4",
                ContinentCode = "NA",
                ContinentName = "North America",
                CountryCode = "US",
                CountryName = "United States",
                RegionCode = "OH",
                RegionName = "Ohio",
                City = "Glenmont",
                Zip = "44628",
                Latitude = 40.5369987487793,
                Longitude = -82.12859344482422,
            };


            return new List<Geolocation>() { location1 };
        }
    }
}
