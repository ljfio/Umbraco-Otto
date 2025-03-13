// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Otto.Core.Extensions;

public static class ContentExtensions
{
    /// <summary>
    /// Checks if the content type of <paramref name="entity"/> matches one of the <paramref name="aliases"/>
    /// </summary>
    /// <param name="entity">entity to check</param>
    /// <param name="aliases">content type aliases to compare</param>
    /// <returns></returns>
    public static bool HasContentType(this IContentBase entity, IReadOnlyCollection<string> aliases) =>
        aliases.Contains(entity.ContentType.Alias);

    /// <summary>
    /// Checks if the content type of <paramref name="entity"/> matches the <paramref name="alias"/>
    /// </summary>
    /// <param name="entity">entity to check</param>
    /// <param name="alias">content type alias to compare</param>
    /// <returns></returns>
    public static bool HasContentType(this IContentBase entity, string alias) =>
        entity.ContentType.Alias.Equals(alias);
}