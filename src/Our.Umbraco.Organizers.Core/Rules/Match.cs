// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Core.Rules;

public class Match<TEntity> where TEntity : class, IContentBase
{
    public Match(TEntity entity, MatchType matchType)
    {
        Entity = entity;
        MatchType = matchType;
    }

    public TEntity Entity { get; }

    public MatchType MatchType { get; }
}