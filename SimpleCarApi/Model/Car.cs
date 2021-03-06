﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimpleCarApi.Model
{
    public class Car 
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)] 
        public int Id { get; set; } //has MongoDb carId counter

        [Required]
        public string Name { get; set; }

        public string Description{ get; set; }

    }
}
