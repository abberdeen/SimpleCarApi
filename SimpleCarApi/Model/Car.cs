﻿using MongoDB.Bson.Serialization.Attributes;
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
        [Required]
        public int  Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description{ get; set; }

    }
}
