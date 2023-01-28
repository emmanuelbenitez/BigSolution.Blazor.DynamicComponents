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

using System.Text.RegularExpressions;

namespace BigSolution.Blazor;

/// <summary>Represents the builder for the HTML attribute <c>style</c>.</summary>
public sealed class StyleBuilder
{
	#region Nested Type: ValueBuilder

	private class ValueBuilder : ValueBuilderBase
	{
		internal ValueBuilder() : base(string.Empty, ";", true) { }
	}

	#endregion

	/// <summary>Appends the specified property.</summary>
	/// <param name="property">The property.</param>
	/// <param name="valueGetter">The value getter.</param>
	/// <param name="conditionGetter">The condition getter.</param>
	public void Append(string property, Func<string?> valueGetter, Func<bool> conditionGetter)
	{
		Requires.Argument(property, nameof(property))
			.IsNotNull()
			.Matches(_stylePropertyNameRegex)
			.Check();
		Requires.Argument(valueGetter, nameof(valueGetter))
			.IsNotNull()
			.Check();
		Requires.Argument(conditionGetter, nameof(conditionGetter))
			.IsNotNull()
			.Check();

		_builder.Append(() => $"{property}:{valueGetter()}", conditionGetter);
	}

	/// <summary>Appends the specified style.</summary>
	/// <param name="style">The style.</param>
	public void Append(string style)
	{
		Requires.Argument(style, nameof(style))
			.Matches(_styleRegex)
			.Check();

		_builder.Append(style);
	}

	/// <summary>Builds this instance.</summary>
	/// <returns>
	///   <br />
	/// </returns>
	public string? Build()
	{
		return _builder.Build();
	}

	/// <summary>Removes all styles from the <see cref="BigSolution.Blazor.StyleBuilder" />.</summary>
	public void Reset()
	{
		_builder.Reset();
	}

	private static readonly Regex _stylePropertyNameRegex = new("^(-)?(-?[a-z]+)+$", RegexOptions.IgnoreCase);

	private static readonly Regex _styleRegex = new(@"((-)?(-?[a-z]+)+):([^\0]+?)(?=;[^;]+:|$)", RegexOptions.IgnoreCase);

	private readonly ValueBuilder _builder = new();
}
