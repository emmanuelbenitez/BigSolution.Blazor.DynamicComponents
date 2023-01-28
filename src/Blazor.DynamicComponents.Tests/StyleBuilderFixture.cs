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

using FluentAssertions;
using Xunit;

namespace BigSolution.Blazor;

public class StyleBuilderFixture
{
	[Fact]
	public void AppendFailedForConditionGetter()
	{
		var styleBuilder = new StyleBuilder();
		var act = () => styleBuilder.Append("test", () => string.Empty, null);

		act.Should().ThrowExactly<ArgumentNullException>().Which.ParamName.Should().Be("conditionGetter");
	}

	[Theory]
	[InlineData("")]
	[InlineData(" ")]
	[InlineData("two parts.")]
	public void AppendFailedForProperty(string property)
	{
		var styleBuilder = new StyleBuilder();
		var act = () => styleBuilder.Append(property, () => string.Empty, () => true);

		act.Should().ThrowExactly<ArgumentException>().Which.ParamName.Should().Be("property");
	}

	[Fact]
	public void AppendFailedForValueGetter()
	{
		var styleBuilder = new StyleBuilder();
		var act = () => styleBuilder.Append("test", null, () => true);

		act.Should().ThrowExactly<ArgumentNullException>().Which.ParamName.Should().Be("valueGetter");
	}

	[Theory]
	[InlineData("test:property")]
	[InlineData("test:property;test: call('this is a example') important!;")]
	public void AppendStyleSucceeds(string style)
	{
		var builder = new StyleBuilder();
		var act = () => builder.Append(style);

		act.Should().NotThrow();
	}

	[Theory]
	[MemberData(nameof(GetValidBuilder))]
	public void BuildSucceeds(StyleBuilder builder, string? expectedStyleValue)
	{
		builder.Build().Should().Be(expectedStyleValue);
	}

	[Fact]
	public void ResetSucceeds()
	{
		var builder = new StyleBuilder();
		builder.Append("property", "value");

		builder.Reset();

		builder.Build().Should().BeNull();
	}

	public static IEnumerable<object?[]> GetValidBuilder()
	{
		yield return new object?[] { new StyleBuilder().Append("test", "value"), "test:value" };
		yield return new object?[] { new StyleBuilder().Append("test", "value").Append("property", "value"), "test:value;property:value" };
		yield return new object?[] { new StyleBuilder().Append("test", "value", false), null };
		yield return new object?[] { new StyleBuilder().Append("test", "value", false).Append("property", "value"), "property:value" };
	}
}
