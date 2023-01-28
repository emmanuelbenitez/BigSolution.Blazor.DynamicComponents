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

/// <summary>Represents the builder for the HTML attribute <c>class</c>.</summary>
public sealed class CssBuilder : ValueBuilderBase
{
	/// <summary>Initializes a new instance of the <see cref="CssBuilder" /> class.</summary>
	/// <param name="prefix">The prefix.</param>
	public CssBuilder(string prefix = "") : base(prefix, DEFAULT_SEPARATOR, true) { }

	private const string DEFAULT_SEPARATOR = " ";
}