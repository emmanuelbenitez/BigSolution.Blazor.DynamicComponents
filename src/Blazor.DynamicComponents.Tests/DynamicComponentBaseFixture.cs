#region Copyright & License

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

using System.Diagnostics.CodeAnalysis;
using BlazorComponentUtilities;
using FluentAssertions;
using Xunit;

namespace BigSolution.Blazor;

[SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.", Justification = "Test Purpose", Scope = "member")]
public class DynamicComponentBaseFixture
{
	[Fact]
	public void SetTagNameFailed()
	{
		var action = () => { new FakeDynamicComponentWhitSupportTagNames().TagName = "a"; };
		action.Should().ThrowExactly<ArgumentOutOfRangeException>().Which.ParamName.Should().Be("value");
	}

	[Fact]
	public void SetTagNameSucceeds()
	{
		var component = new FakeDynamicComponentWhitSupportTagNames { TagName = "tagName" };
		component.TagName.Should().Be("tagName");
	}

	private class FakeDynamicComponentWhitSupportTagNames : DynamicComponentBase
	{
		#region Base Class Member Overrides

		protected override CssBuilder CssBuilder => new();

		protected override StyleBuilder StyleBuilder => new();

		protected override IEnumerable<string> SupportedTagNames => new[] { "tagName" };

		#endregion
	}
}