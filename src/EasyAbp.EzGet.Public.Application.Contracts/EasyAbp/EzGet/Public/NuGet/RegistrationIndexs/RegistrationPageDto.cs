﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EasyAbp.EzGet.Public.NuGet.RegistrationIndexs
{
    public class RegistrationPageDto
    {
        [JsonPropertyName("@id")]
        [JsonProperty("@id")]
        public string RegistrationPageUrl { get; set; }

        public int Count { get; set; }

        public string Lower { get; set; }

        public string Upper { get; set; }

        [JsonPropertyName("items")]
        [JsonProperty("items")]
        public IReadOnlyList<RegistrationPageItemDto> PageItems { get; set; }
    }
}
