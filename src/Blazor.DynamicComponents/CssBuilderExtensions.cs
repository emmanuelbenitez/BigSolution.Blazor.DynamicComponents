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

/// <summary>Provides extensions for <see cref="CssBuilder" />.</summary>
public static class CssBuilderExtensions
{
	/// <summary>Appends the class from attributes.</summary>
	/// <param name="builder">The builder.</param>
	/// <param name="attributes">The attributes.</param>
	/// <returns>The builder.</returns>
	public static CssBuilder AppendFromAttributes(this CssBuilder builder, IReadOnlyDictionary<string, object?>? attributes)
	{
		if (attributes != null) builder.Append(() => attributes[CLASS_ATTRIBUTE_NAME]?.ToString() ?? string.Empty, () => attributes.ContainsKey(CLASS_ATTRIBUTE_NAME));
		return builder;
	}

	private const string CLASS_ATTRIBUTE_NAME = "class";
}