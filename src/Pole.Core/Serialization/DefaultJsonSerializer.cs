﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Pole.Core.Serialization
{
    public class DefaultJsonSerializer : ISerializer
    {
        static readonly JsonSerializerOptions options = new JsonSerializerOptions() { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), MaxDepth = 5, ReferenceHandling = ReferenceHandling.Preserve, WriteIndented =true};
        public T Deserialize<T>(string json) where T : class, new()
        {
            return JsonSerializer.Deserialize<T>(json);
        }

        public object Deserialize(byte[] bytes, Type type)
        {
            return JsonSerializer.Deserialize(bytes, type);
        }
        public string Serialize<T>(T data) where T : class, new()
        {
            return JsonSerializer.Serialize(data, options);
        }
        public string Serialize(object data, Type type)
        {
            return JsonSerializer.Serialize(data, type, options);
        }
        public byte[] SerializeToUtf8Bytes<T>(T data) where T : class, new()
        {
            return JsonSerializer.SerializeToUtf8Bytes(data, data.GetType(), options);
        }

        public T Deserialize<T>(byte[] bytes) where T : class, new()
        {
            return JsonSerializer.Deserialize<T>(bytes);
        }

        public byte[] SerializeToUtf8Bytes(object data, Type type)
        {
            return JsonSerializer.SerializeToUtf8Bytes(data, type, options);
        }

        public object Deserialize(string json, Type type)
        {
            return JsonSerializer.Deserialize(json, type);
        }
    }
}
