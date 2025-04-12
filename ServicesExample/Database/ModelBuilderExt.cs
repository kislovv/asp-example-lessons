﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ServicesExample.Database;

public static class ModelBuilderExt
{
    public static PropertyBuilder<TProperty> HasMinLength<TProperty>(
        this PropertyBuilder<TProperty> builder,
        int minLength)
    {
        builder.Metadata.AddAnnotation("MinimumLength", minLength);

        return builder;
    }
}