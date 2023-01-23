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

using System;
using System.Collections.Generic;
using System.Linq;
using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BigSolution.Blazor
{
	/// <summary>
	/// Defines a dynamic component. Its support unmatched attributes, CSS classes based on <see cref="CssBuilder"/>.
	/// </summary>
	public abstract class DynamicComponentBase : ComponentBase
	{
		/// <summary>
		/// All unmatched attributes
		/// </summary>
		[Parameter(CaptureUnmatchedValues = true)]
		public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }

		/// <summary>
		/// Gets or sets additional CSS classes
		/// </summary>
		[Parameter]
		[Obsolete("Please use the attribute 'class'.")]
		public string Classes { get; set; }

		/// <summary>
		/// Gets the generated CSS classes
		/// </summary>
		public string CssClasses => CssBuilder
			.AddClass(Classes, !string.IsNullOrWhiteSpace(Classes))
			.AddClassFromAttributes(AdditionalAttributes)
			.NullIfEmpty();

		/// <summary>Gets the style.</summary>
		/// <value>The style.</value>
		public string Style => StyleBuilder
			.AddStyleFromAttributes(AdditionalAttributes)
			.NullIfEmpty();

		/// <summary>
		/// Gets or sets the HTML tag name
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when the value is not in <see cref="SupportedTagNames"/>.</exception>
		[Parameter]
		public string TagName
		{
			get => _tagName ?? DefaultTagName;
			set
			{
				if (SupportedTagNames.Any() && !SupportedTagNames.Contains(value))
				{
					throw new ArgumentOutOfRangeException(
						nameof(value),
						value,
						$"The tag name '{value}' is not supported (Supported values: ${string.Join(",", SupportedTagNames)}).");
				}
				_tagName = value;
			}
		}

		/// <summary>
		/// Gets the CSS builder.
		/// </summary>
		protected abstract CssBuilder CssBuilder { get; }

		/// <summary>
		/// Gets the default HTML tag name.
		/// </summary>
		protected virtual string DefaultTagName => "div";

		/// <summary>Gets the style builder.</summary>
		/// <value>The style builder.</value>
		protected abstract StyleBuilder StyleBuilder { get; }

		/// <summary>
		/// Gets the supported HTML tag names.
		/// </summary>
		protected virtual IEnumerable<string> SupportedTagNames => Enumerable.Empty<string>();

		private string _tagName;
	}
}
