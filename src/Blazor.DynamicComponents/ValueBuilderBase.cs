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

namespace BigSolution.Blazor;

/// <summary>Represents a class base fo a builder.</summary>
public abstract class ValueBuilderBase
{
	#region Nested Type: ValuePart

	private class ValuePart
	{
		public ValuePart(Func<string> valueGetter, Func<bool> conditionGetter)
		{
			ValueGetter = valueGetter;
			ConditionGetter = conditionGetter;
		}

		public Func<bool> ConditionGetter { get; }

		public Func<string> ValueGetter { get; }
	}

	#endregion

	/// <summary>Initializes a new instance of the <see cref="ValueBuilderBase" /> class.</summary>
	/// <param name="prefix">The prefix.</param>
	/// <param name="separator">The separator between parts.</param>
	/// <param name="returnsNull">if set to <c>true</c>, returns <see langword="null"/> if the build result is empty; otherwise, <see cref="string.Empty"/>.</param>
	protected ValueBuilderBase(string prefix, string separator, bool returnsNull)
	{
		_prefix = prefix;
		_separator = separator;
		_returnsNull = returnsNull;
		Initialize();
	}

	/// <summary>Appends the value based on the specified function.</summary>
	/// <param name="valueGetter">The function to get the value.</param>
	/// <param name="conditionGetter">The function to get if the value will be appended during the build.</param>
	/// <returns>The builder.</returns>
	public void Append(Func<string> valueGetter, Func<bool> conditionGetter)
	{
		_parts.Add(new ValuePart(valueGetter, conditionGetter));
	}

	/// <summary>Builds the CSS class.</summary>
	/// <returns>The CSS class.</returns>
	public string? Build()
	{
		var builtParts = _parts
			.Where(part => part.ConditionGetter())
			.Select(part => part.ValueGetter())
			.Where(value => !string.IsNullOrWhiteSpace(value))
			.ToArray();

		return builtParts.Any() || !_returnsNull ? string.Join(_separator, builtParts) : null;
	}

	private void Initialize()
	{
		Append(() => _prefix, () => true);
	}

	/// <summary>Removes all parts from the <see cref="BigSolution.Blazor.CssClassBuilder" />.</summary>
	public void Reset()
	{
		_parts.Clear();
		Initialize();
	}

	private readonly List<ValuePart> _parts = new();

	private readonly string _prefix;
	private readonly bool _returnsNull;

	private readonly string _separator;
}
