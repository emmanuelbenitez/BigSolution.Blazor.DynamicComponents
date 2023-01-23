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

using System.Diagnostics.CodeAnalysis;
using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BigSolution.Blazor;

/// <summary>
/// Defines a dynamic HTML element
/// </summary>
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global", Justification = "Public API.")]
public class DynamicElement : DynamicComponentWithBodyBase
{
	#region Base Class Member Overrides

	/// <inheritdoc />
	protected override void BuildRenderTree(RenderTreeBuilder builder)
	{
		Requires.Argument(builder, nameof(builder))
			.IsNotNull()
			.Check();
		var sequenceGenerator = new SequenceGenerator();

		builder.OpenElement(sequenceGenerator.GetNextValue(), TagName);
		builder.AddAttribute(sequenceGenerator.GetNextValue(), CLASS_ATTRIBUTE_NAME, CssClasses);
		builder.AddAttribute(sequenceGenerator.GetNextValue(), STYLE_ATTRIBUTE_NAME, Style);
		builder.AddMultipleAttributes(sequenceGenerator.GetNextValue(), Attributes.Where(pair => !AdditionalAttributesExcludedFromRendering.Contains(pair.Key)));
		BuildRenderTreeForChildContent(sequenceGenerator, () => builder.AddContent(sequenceGenerator.GetNextValue(), ChildContent));
		builder.AddElementReferenceCapture(5, elementReference => { Element = elementReference; });
		builder.CloseElement();
	}

	#endregion

	#region Base Class Member Overrides

	/// <inheritdoc />
	protected override CssBuilder CssBuilder => new();

	/// <inheritdoc />
	protected override StyleBuilder StyleBuilder => new();

	#endregion

	/// <summary>
	/// Gets or sets the associated <see cref="T:Microsoft.AspNetCore.Components.ElementReference" />.
	/// <para>
	/// May be <see langword="null" /> if accessed before the component is rendered.
	/// </para>
	/// </summary>
	public ElementReference? Element { get; [param: DisallowNull] private set; }

	/// <summary>
	/// Gets the additional attributes excluded from rendering.
	/// </summary>
	/// <value>The additional attributes excluded from rendering.</value>
	protected virtual IEnumerable<string> AdditionalAttributesExcludedFromRendering => new[] { CLASS_ATTRIBUTE_NAME, STYLE_ATTRIBUTE_NAME };

	/// <summary>
	/// Gets the attributes.
	/// </summary>
	/// <value>The attributes.</value>
	protected virtual IEnumerable<KeyValuePair<string, object>> Attributes => AdditionalAttributes ?? Enumerable.Empty<KeyValuePair<string, object>>();

	/// <summary>
	/// Builds the content of the render tree for child.
	/// </summary>
	/// <param name="sequenceGenerator">The sequence generator.</param>
	/// <param name="renderContent">Content of the render.</param>
	[SuppressMessage("ReSharper", "UnusedParameter.Global", Justification = "Public API.")]
	protected virtual void BuildRenderTreeForChildContent(SequenceGenerator sequenceGenerator, Action renderContent)
	{
		renderContent();
	}

	private const string CLASS_ATTRIBUTE_NAME = "class";
	private const string STYLE_ATTRIBUTE_NAME = "style";
}