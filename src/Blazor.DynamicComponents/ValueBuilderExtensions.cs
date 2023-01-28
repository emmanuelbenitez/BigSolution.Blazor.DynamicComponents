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

/// <summary>Provides extensions for <see cref="ValueBuilderBase" />.</summary>
public static class ValueBuilderExtensions
{
	/// <summary>Appends the specified value.</summary>
	/// <typeparam name="TValueBuilder">The type of the value builder.</typeparam>
	/// <param name="builder">The builder.</param>
	/// <param name="value">The value.</param>
	/// <param name="condition">if set to <c>true</c>, the value will be added during the build.</param>
	/// <returns>The builder.</returns>
	public static TValueBuilder Append<TValueBuilder>(this TValueBuilder builder, string value, bool condition = true)
		where TValueBuilder : ValueBuilderBase
	{
		builder.Append(() => value, () => condition);
		return builder;
	}

	/// <summary>Appends the specified value.</summary>
	/// <typeparam name="TValueBuilder">The type of the value builder.</typeparam>
	/// <param name="builder">The builder.</param>
	/// <param name="value">The value.</param>
	/// <param name="conditionGetter">The function to get if the value will be appended during the build.</param>
	/// <returns>The builder.</returns>
	public static TValueBuilder Append<TValueBuilder>(this TValueBuilder builder, string value, Func<bool> conditionGetter)
		where TValueBuilder : ValueBuilderBase
	{
		builder.Append(() => value, conditionGetter);
		return builder;
	}

	/// <summary>Appends the value based on the specified function.</summary>
	/// <typeparam name="TValueBuilder">The type of the value builder.</typeparam>
	/// <param name="builder">The builder.</param>
	/// <param name="valueGetter">The function to get the value.</param>
	/// <param name="condition">if set to <c>true</c>, the value will be added during the build.</param>
	/// <returns>The builder.</returns>
	[PublicAPI]
	public static TValueBuilder Append<TValueBuilder>(this TValueBuilder builder, Func<string> valueGetter, bool condition = true)
		where TValueBuilder : ValueBuilderBase
	{
		builder.Append(valueGetter, () => condition);
		return builder;
	}

	/// <summary>Appends the value based on the specified function.</summary>
	/// <typeparam name="TValueBuilder">The type of the value builder.</typeparam>
	/// <param name="builder">The builder.</param>
	/// <param name="valueGetter">The function to get the value.</param>
	/// <param name="conditionGetter">The function to get if the value will be appended during the build.</param>
	/// <returns>The builder.</returns>
	[PublicAPI]
	public static TValueBuilder Append<TValueBuilder>(this TValueBuilder builder, Func<string> valueGetter, Func<bool> conditionGetter)
		where TValueBuilder : ValueBuilderBase
	{
		builder.Append(valueGetter, conditionGetter);
		return builder;
	}
}