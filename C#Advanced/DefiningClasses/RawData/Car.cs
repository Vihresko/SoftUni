﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RawData
{
    public class Car
    {
        public string Model{ get; set; }
        public Engine Engine { get; set; }
        public Cargo Cargo { get; set; }

        public List<Tire> Tires { get; set; }

        public Car()
        {
            Tires = new List<Tire>();
        }

        public Car(string model, Engine engine, Cargo cargo, List<Tire> tires) :this()
        {
            Model = model;
            Engine = engine;
            Cargo = cargo;
            Tires = tires;
        }
    }
}
