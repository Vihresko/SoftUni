﻿namespace SharedTrip.Models
{
    public class TripAddForm
    {
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string DepartureTime { get; set; }
        public string ImagePath { get; set; }
        public int Seats { get; set; }
        public string Description { get; set; }
    }
}
