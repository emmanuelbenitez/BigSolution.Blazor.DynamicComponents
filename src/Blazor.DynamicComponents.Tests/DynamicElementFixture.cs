﻿#region Copyright & License

// Copyright © 2020 - 2021 Emmanuel Benitez
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

using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using AngleSharp.Dom;
using Bunit;
using FluentAssertions;
using Xunit;

namespace BigSolution.Blazor;

[SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.", Justification = "Test Purpose", Scope = "member")]
public class DynamicElementFixture : TestContext
{
	[Fact]
	public void ClassAttributeRendered()
	{
		var renderedComponent = RenderComponent<DynamicElement>(ComponentParameter.CreateParameter(nameof(DynamicElement.Classes), "test"));
		renderedComponent.Instance.Element.Should().NotBeNull();
	}

	[Fact]
	public void ElementIsNotNullAfterRendering()
	{
		var renderedComponent = RenderComponent<DynamicElement>(builder => builder.AddUnmatched("id", "test"));
		renderedComponent.Instance.Element.Should().NotBeNull();
	}

	[Fact]
	public void ClassAttributeNotRendered()
	{
		var renderedComponent = RenderComponent<DynamicElement>();
		renderedComponent.Find("div").ClassName.Should().BeNull();
		renderedComponent.Find("div").Attributes.GetNamedItem("style").Should().BeNull();
	}

	[Fact]
	public void StyleAttributeNotRendered()
	{
		var renderedComponent = RenderComponent<DynamicElement>();
		renderedComponent.Find("div").Attributes.GetNamedItem("style").Should().BeNull();
	}

	[Fact]
	public void StyleAttributeRendered()
	{
		var renderedComponent = RenderComponent<DynamicElement>(builder => builder.AddUnmatched("style", "test:value"));
		renderedComponent.Find("div").Attributes.GetNamedItem("style").Should().NotBeNull()
			.And.Subject.As<IAttr>().Value.Should().Be("test:value");
	}

	[Fact]
	public void CssClassesWellFormatted()
	{
		new DynamicElement {
			Classes = "test",
			AdditionalAttributes = new ReadOnlyDictionary<string, object>(new Dictionary<string, object> { { "class", "value" } })
		}.CssClasses.Should().Be("test value");
	}

	[Fact]
	public void StyleWellFormatted()
	{
		new DynamicElement
		{
			AdditionalAttributes = new ReadOnlyDictionary<string, object>(new Dictionary<string, object> { { "style", "property:value" } })
		}.Style.Should().Be("property:value");
	}

	[Fact]
	public void TagNameRendered()
	{
		var renderedComponent = RenderComponent<DynamicElement>(ComponentParameter.CreateParameter(nameof(DynamicElement.TagName), "test"));
		renderedComponent.Find("test").Should().NotBeNull();
	}
}