#region Copyright & License

// Copyright © 2020 - 2023 Emmanuel Benitez
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using JetBrains.Annotations;

namespace BigSolution.Blazor;

/// <summary>Provides extensions for <see cref="StyleBuilder" />.</summary>
public static class StyleBuilderExtensions
{
	/// <summary>Appends the class from attributes.</summary>
	/// <param name="builder">The builder.</param>
	/// <param name="attributes">The attributes.</param>
	/// <returns>The builder.</returns>
	public static StyleBuilder AppendFromAttributes(this StyleBuilder builder, IReadOnlyDictionary<string, object?>? attributes)
	{
		if (attributes != null && attributes.TryGetValue(STYLE_ATTRIBUTE_NAME, out var style )) builder.Append(style?.ToString() ?? string.Empty);
		return builder;
	}

	/// <summary>Appends the specified style property.</summary>
	/// <param name="builder">The builder.</param>
	/// <param name="property">The style property.</param>
	/// <param name="value">The value.</param>
	/// <param name="condition">if set to <c>true</c>, the value will be added during the build.</param>
	/// <returns>The builder.</returns>
	public static StyleBuilder Append(this StyleBuilder builder, string property, string value, bool condition = true)
	{
		builder.Append(property, () => value, () => condition);
		return builder;
	}

	/// <summary>Appends the specified style property.</summary>
	/// <param name="builder">The builder.</param>
	/// <param name="property">The style property.</param>
	/// <param name="value">The value.</param>
	/// <param name="conditionGetter">The function to get if the value will be appended during the build.</param>
	/// <returns>The builder.</returns>
	[PublicAPI]
	public static StyleBuilder Append(this StyleBuilder builder, string property, string value, Func<bool> conditionGetter)
	{
		builder.Append(property, () => value, conditionGetter);
		return builder;
	}

	/// <summary>Appends the value based on the specified function.</summary>
	/// <param name="builder">The builder.</param>
	/// <param name="property">The style property.</param>
	/// <param name="valueGetter">The function to get the value.</param>
	/// <param name="condition">if set to <c>true</c>, the value will be added during the build.</param>
	/// <returns>The builder.</returns>
	[PublicAPI]
	public static StyleBuilder Append(this StyleBuilder builder, string property, Func<string> valueGetter, bool condition = true)
	{
		builder.Append(property, valueGetter, () => condition);
		return builder;
	}

	/// <summary>Appends the value based on the specified function.</summary>
	/// <param name="builder">The builder.</param>
	/// <param name="property">The style property.</param>
	/// <param name="valueGetter">The function to get the value.</param>
	/// <param name="conditionGetter">The function to get if the value will be appended during the build.</param>
	/// <returns>The builder.</returns>
	[PublicAPI]
	public static StyleBuilder Append(this StyleBuilder builder, string property, Func<string> valueGetter, Func<bool> conditionGetter)
	{
		builder.Append(property, valueGetter, conditionGetter);
		return builder;
	}

	private const string STYLE_ATTRIBUTE_NAME = "style";
}
