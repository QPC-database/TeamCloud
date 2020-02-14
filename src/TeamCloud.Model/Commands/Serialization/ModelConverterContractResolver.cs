﻿/**
 *  Copyright (c) Microsoft Corporation.
 *  Licensed under the MIT License.
 */

using System;
using System.Collections;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TeamCloud.Model.Commands.Serialization
{
    public class ModelConverterContractResolver : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(Exception).IsAssignableFrom(objectType))
                return new ExceptionConverter();

            return base.ResolveContractConverter(objectType);
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);

            if (member is PropertyInfo propertyInfo)
            {
                if (!prop.Writable)
                {
                    // enable private property setter deserialization
                    prop.Writable = propertyInfo.GetSetMethod(true) != null;
                }

                // suppress serialization of empty enumerations
                if (propertyInfo.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    Predicate<object> shouldSerializeEnumerable = obj =>
                    {
                        var enumerable = prop.ValueProvider.GetValue(obj) as IEnumerable;
                        return enumerable?.GetEnumerator().MoveNext() ?? false;
                    };

                    prop.ShouldSerialize = prop.ShouldSerialize == null
                        ? shouldSerializeEnumerable
                        : obj => prop.ShouldSerialize(obj) && shouldSerializeEnumerable(obj);
                }
            }

            return prop;
        }
    }
}
